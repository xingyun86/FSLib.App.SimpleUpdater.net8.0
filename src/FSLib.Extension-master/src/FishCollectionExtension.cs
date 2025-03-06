using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace System
{
	/// <summary>
	/// 集合类的扩展方法
	/// </summary>
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public static class FishCollectionExtension
	{
#if !NETSTANDARD1_6_1 && !NETSTANDARD2_0 && !NETSTANDARD3_0
		#region NameValueCollection

		/// <summary>
		/// 转换为字典
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		public static Dictionary<string, string> ToDictionary(this NameValueCollection collection)
		{
			var dic = new Dictionary<string, string>(collection == null ? 10 : collection.Count, StringComparer.OrdinalIgnoreCase);
			if (collection != null)
			{
				collection.AllKeys.ForEach(s => dic.Add(s, collection[s]));
			}

			return dic;
		}

		/// <summary>
		/// 从集合中获得指定的键值
		/// </summary>
		/// <param name="collection"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetValue(this NameValueCollection collection, string key)
		{
			return GetValue(collection, key, string.Empty);
		}

		/// <summary>
		/// 从集合中获得指定的键值
		/// </summary>
		/// <param name="collection"></param>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static string GetValue(this NameValueCollection collection, string key, string defaultValue)
		{
			if (string.IsNullOrEmpty(key))
				throw new ArgumentNullException("key");

			if (collection == null)
				return defaultValue;

			var e = collection[key];

			if (e == null)
				return defaultValue;

			return e;
		}


		#endregion
#endif

		#region 列表

		//	/// <summary>
		//	/// 在一个序列中查找指定的项出现的位置
		//	/// </summary>
		//	/// <typeparam name="T"></typeparam>
		//	/// <param name="source"></param>
		//	/// <param name="target"></param>
		//	/// <param name="pos"></param>
		//	/// <param name="length"></param>
		//	/// <returns></returns>
		//	public static int IndexOf<T>(this IList<T> source, T target, int pos, int length)
		//where T : IEquatable<T>
		//	{
		//		for (int i = pos; i < pos + length; i++)
		//		{
		//			if (source[i].Equals(target))
		//				return i;
		//		}

		//		return -1;
		//	}

		//	/// <summary>
		//	/// 判断指定的序列是不是用特定的一部分值开始的
		//	/// </summary>
		//	/// <typeparam name="T">序列类型</typeparam>
		//	/// <param name="source">源序列</param>
		//	/// <param name="mark">要查找的目标</param>
		//	/// <returns></returns>
		//	public static int StartsWith<T>(this IList<T> source, T[] mark)
		//		where T : IEquatable<T>
		//	{
		//		return source.StartsWith(0, source.Count, mark);
		//	}

		//	/// <summary>
		//	/// 判断指定的序列是不是用特定的一部分值开始的
		//	/// </summary>
		//	/// <typeparam name="T">序列类型</typeparam>
		//	/// <param name="source">源序列</param>
		//	/// <param name="mark">要查找的目标</param>
		//	/// <param name="offset">开始查找的位置</param>
		//	/// <param name="length">要匹配的长度</param>
		//	/// <returns></returns>
		//	public static int StartsWith<T>(this IList<T> source, int offset, int length, T[] mark)
		//		where T : IEquatable<T>
		//	{
		//		int pos = offset;
		//		int endOffset = offset + length - 1;

		//		for (int i = 0; i < mark.Length; i++)
		//		{
		//			int checkPos = pos + i;

		//			if (checkPos > endOffset)
		//				return i;

		//			if (!source[checkPos].Equals(mark[i]))
		//				return -1;
		//		}

		//		return mark.Length;
		//	}

		//	public static bool EndsWith<T>(this IList<T> source, T[] mark)
		//		where T : IEquatable<T>
		//	{
		//		return source.EndsWith(0, source.Count, mark);
		//	}

		//	public static bool EndsWith<T>(this IList<T> source, int offset, int length, T[] mark)
		//		where T : IEquatable<T>
		//	{
		//		if (mark.Length > length)
		//			return false;

		//		for (int i = 0; i < Math.Min(length, mark.Length); i++)
		//		{
		//			if (!mark[i].Equals(source[offset + length - mark.Length + i]))
		//				return false;
		//		}

		//		return true;
		//	}

		//	public static T[] CloneRange<T>(this T[] source, int offset, int length)
		//	{
		//		T[] target = new T[length];
		//		Array.Copy(source, offset, target, 0, length);
		//		return target;
		//	}

		private static Random m_Random = new Random();

		/// <summary>
		/// 将一个数组内容随机打乱
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static T[] RandomOrder<T>(this T[] source)
		{
			var n = source.Length / 2;

			for (var i = 0; i < n; i++)
			{
				var x = m_Random.Next(0, source.Length - 1);
				var y = m_Random.Next(0, source.Length - 1);

				if (x == y)
					continue;

				var t = source[y];

				//swap position
				source[y] = source[x];
				source[x] = t;
			}

			return source;
		}


		/// <summary>
		/// 对泛型列表的数据进行分页
		/// </summary>
		/// <typeparam name="T">列表的类型</typeparam>
		/// <param name="obj">原始数据</param>
		/// <param name="pagesize">每页尺寸</param>
		/// <param name="pageindex">页码，引用型，当超过范围时会自动修正</param>
		/// <param name="totalpage">传出，表示当前共有多少页</param>
		/// <returns>分页后的结果</returns>
		public static T[] SplitPage<T>(this IList<T> obj, int pagesize, ref int pageindex, out int totalpage)
		{
			T[] result;
			int count;

			if (pagesize < 1)
				throw new System.ArgumentOutOfRangeException("pagesize", "每页尺寸不得小于1");

			totalpage = obj.Count.CeilingDivide(pagesize);
			if (pageindex < 1)
				pageindex = 1;
			else if (pageindex > totalpage)
				pageindex = totalpage;

			if (totalpage * pagesize > obj.Count)
				count = obj.Count - totalpage * (pagesize - 1);
			else
				count = pagesize;

			result = new T[count];

			int startIndex = pagesize * (pageindex - 1);
			for (int i = 0; i < count; i++)
			{
				result[i] = obj[i + startIndex];
			}

			return result;
		}

		/// <summary>
		/// 从一个列表中随机选择对象
		/// </summary>
		/// <typeparam name="T">队列数据类型</typeparam>
		/// <param name="list">列表</param>
		/// <param name="random">使用的随机种子。如果使用null，则会新建一个</param>
		/// <returns>获得的结果</returns>
		public static T RandomTake<T>(this List<T> list, Random random = null)
		{
			if (list == null || list.Count == 0)
				return default(T);

			return list[(random ?? new Random()).Next(list.Count)];
		}

		#endregion

		#region 字典

		/// <summary>
		/// 尝试从字典中获得值
		/// </summary>
		/// <typeparam name="Tkey">字典的键类型</typeparam>
		/// <typeparam name="TValue">字典的值类型</typeparam>
		/// <param name="dic">字典对象</param>
		/// <param name="key">检索的键</param>
		/// <returns>返回要查找的对象</returns>
		public static TValue GetValue<Tkey, TValue>(this Dictionary<Tkey, TValue> dic, Tkey key)
		{
			return dic.GetValue(key, default(TValue));
		}

		/// <summary>
		/// 尝试从字典中获得值, 如果不包含则放入一个由指定函数返回的初始值
		/// </summary>
		/// <typeparam name="Tkey">字典的键类型</typeparam>
		/// <typeparam name="TValue">字典的值类型</typeparam>
		/// <param name="dic">字典对象</param>
		/// <param name="key">检索的键</param>
		/// <returns>返回要查找的对象</returns>
		public static TValue GetValue<Tkey, TValue>(this Dictionary<Tkey, TValue> dic, Tkey key, Func<Tkey, TValue> initialValueFunc)
		{
			TValue value;

			if (!dic.TryGetValue(key, out value) && initialValueFunc != null)
			{
				lock (dic)
				{
					if (!dic.TryGetValue(key, out value))
						dic.Add(key, value = initialValueFunc(key));
				}
			}

			return value;
		}

		/// <summary>
		/// 尝试从字典中获得值
		/// </summary>
		/// <typeparam name="Tkey">字典的键类型</typeparam>
		/// <typeparam name="TValue">字典的值类型</typeparam>
		/// <param name="dic">字典对象</param>
		/// <param name="key">检索的键</param>
		/// <param name="defaultValue">没有找到时返回的默认值</param>
		/// <returns>返回要查找的对象</returns>
		public static TValue GetValue<Tkey, TValue>(this Dictionary<Tkey, TValue> dic, Tkey key, TValue defaultValue)
		{
			TValue value = default(TValue);
			if (!dic.TryGetValue(key, out value))
				value = defaultValue;

			return value;
		}

		/// <summary>
		/// 查找一个有序字典中指定的键的索引
		/// </summary>
		/// <typeparam name="TKey">字典的键类型</typeparam>
		/// <typeparam name="TValue">字典的值类型</typeparam>
		/// <param name="dic">字典</param>
		/// <param name="key">要查找的键</param>
		/// <returns>返回相应的索引；如果没找到，则返回 -1</returns>
		public static int GetIndex<TKey, TValue>(this SortedDictionary<TKey, TValue> dic, TKey key)
		{
			if (dic == null)
				return -1;

			var idx = 0;
			foreach (var keyidx in dic.Keys)
			{
				if (keyidx.Equals(key))
					return idx;
				idx++;
			}

			return -1;
		}

		/// <summary>
		/// 查找一个有序列表中指定的键的索引
		/// </summary>
		/// <typeparam name="TKey">键类型</typeparam>
		/// <typeparam name="TValue">值类型</typeparam>
		/// <param name="list">列表</param>
		/// <param name="key">要查找的键</param>
		/// <returns>返回相应的索引；如果没找到，则返回 -1</returns>
		public static int GetIndex<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key)
		{
			if (list == null)
				return -1;

			var idx = 0;
			foreach (var keyidx in list.Keys)
			{
				if (keyidx.Equals(key))
					return idx;
				idx++;
			}

			return -1;
		}

		/// <summary>
		/// 添加或更新一个值
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dic"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue value)
		{
			if (dic.ContainsKey(key))
				dic[key] = value;
			else dic.Add(key, value);
		}

		/// <summary>
		/// 尝试移除一个键
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dic"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, out TValue value)
		{
			value = default(TValue);
			if (dic.ContainsKey(key))
			{
				lock (dic)
				{
					if (dic.ContainsKey(key))
					{
						value = dic[key];
						dic.Remove(key);

						return true;
					}

				}
			}
			return false;
		}

		#endregion

		#region Enumerable

		/// <summary>
		/// 将指定的序列转换为目标序列
		/// </summary>
		/// <typeparam name="TIn"></typeparam>
		/// <typeparam name="TOut"></typeparam>
		/// <param name="src"></param>
		/// <param name="targetObj"></param>
		/// <returns></returns>
		public static IEnumerable<TOut> CastToAnonymousType<TIn, TOut>(this IEnumerable<TIn> src, TOut targetObj)
		{
			return src.Cast<TOut>();
		}

		/// <summary>
		/// 对队列进行过滤, 去除为空的项目
		/// </summary>
		/// <typeparam name="T">队列类型</typeparam>
		/// <param name="source">来源</param>
		/// <returns></returns>
		public static IEnumerable<T> ExceptNull<T>(this IEnumerable<T> source)
			where T : class
		{
			return source.Where(s => s != null);
		}

		/// <summary>
		/// 转换队列到HashSet对象
		/// </summary>
		/// <typeparam name="T">可枚举类型</typeparam>
		/// <param name="source">源</param>
		/// <returns></returns>
		public static HashSet<T> MapToHashSet<T>(this IEnumerable<T> source)
		{
			return new HashSet<T>(source);
		}

		/// <summary>
		/// 转换队列到HashSet对象
		/// </summary>
		/// <typeparam name="T">可枚举类型</typeparam>
		/// <param name="source">源</param>
		/// <param name="comparer">要使用的比较器</param>
		/// <returns><see cref="T:System.Collections.Generic.HashSet{T}"/></returns>
		public static HashSet<T> MapToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
		{
			return new HashSet<T>(source, comparer);
		}


		/// <summary>
		/// 生成两个序列的交叉
		/// </summary>
		/// <typeparam name="T1">序列1类型</typeparam>
		/// <typeparam name="T2">序列2类型</typeparam>
		/// <param name="t1">序列1</param>
		/// <param name="t2">序列2</param>
		/// <returns></returns>
		public static IEnumerable<KeyValuePair<T1, T2>> CrossJoin<T1, T2>(this IEnumerable<T1> t1, IEnumerable<T2> t2)
		{
			foreach (var t in t1)
			{
				foreach (var tt in t2)
				{
					yield return new KeyValuePair<T1, T2>(t, tt);
				}
			}
		}

		/// <summary>
		/// 将指定的可遍历序列转换为一个队列
		/// </summary>
		/// <typeparam name="T">序列类型</typeparam>
		/// <param name="source">源序列</param>
		/// <returns><see cref="T:System.Collections.Generic.Queue{T}"/></returns>
		public static Queue<T> ToQueue<T>(this IEnumerable<T> source)
		{
			return new Queue<T>(source);
		}

		/// <summary>
		/// 将指定的可遍历序列转换为一个栈
		/// </summary>
		/// <typeparam name="T">序列类型</typeparam>
		/// <param name="source">源序列</param>
		/// <returns><see cref="System.Collections.Generic.Stack{T}"/></returns>
		public static Stack<T> ToStack<T>(this IEnumerable<T> source)
		{
			return new Stack<T>(source);
		}

		/// <summary>
		/// 将源数据按指定的尺寸分割，并返回最终的结果
		/// </summary>
		/// <typeparam name="T">源类型</typeparam>
		/// <param name="source">来源数据</param>
		/// <param name="pagesize">每页尺寸</param>
		/// <returns>包含了每页数据的列表</returns>
		/// <exception cref="ArgumentOutOfRangeException"><c>pagesize</c> is out of range.</exception>
		/// <exception cref="ArgumentNullException">source</exception>
		public static List<T[]> SplitPage<T>(this IEnumerable<T> source, int pagesize)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (pagesize < 1)
				throw new ArgumentOutOfRangeException("pagesize");

			var index = 0;
			var count = source.Count();
			var result = new List<T[]>(count.CeilingDivide(pagesize));

			while (index < count)
			{
				result.Add(source.Skip(index).Take(pagesize).ToArray());
				index += pagesize;
			}

			return result;
		}

		/// <summary>
		/// 将多个对象同时压入队列
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="queue">队列</param>
		/// <param name="eles">对象</param>
		public static void EnqueueMany<T>(this Queue<T> queue, IEnumerable<T> eles)
		{
			eles.ForEach(queue.Enqueue);
		}

		/// <summary>
		/// 获得指定序列中所有长度（从1到序列长度）的排列组合结果
		/// </summary>
		/// <typeparam name="T[]">序列类型</typeparam>
		/// <param name="src">源序列</param>
		/// <returns>
		/// 所有长度（从1到序列长度）的排列组合结果
		/// </returns>
		public static IEnumerable<T[]> GetAllCombinations<T>(this IEnumerable<T> src)
		{
			var list = src.ToArray();
			var pos = Enumerable.Range(0, list.Length).ToArray();

			for (int i = 0; i <= pos.Length; i++)
			{
				foreach (var ret in GetAllCombinations(pos, i, null))
					yield return ret.Select(s => list[s]).ToArray();
			}
		}


		/// <summary>
		/// 获得指定序列中指定长度的排列组合结果
		/// </summary>
		/// <typeparam name="T[]">序列类型</typeparam>
		/// <param name="src">源序列</param>
		/// <param name="length">添加要求的结果序列长度</param>
		/// <returns>
		/// 指定长度的排列组合结果
		/// </returns>
		public static IEnumerable<T[]> GetAllCombinations<T>(this IEnumerable<T> src, int length)
		{
			var list = src.ToArray();
			var pos = Enumerable.Range(0, list.Length).ToArray();

			foreach (var ret in GetAllCombinations(pos, length, null))
				yield return ret.Select(s => list[s]).ToArray();
		}

		static IEnumerable<int[]> GetAllCombinations(int[] src, int length, HashSet<int> pos)
		{
			if (pos == null)
				pos = new HashSet<int>();

			for (int i = 0; i < src.Length; i++)
			{
				if (!pos.Add(i))
					continue;

				if (pos.Count < length)
				{
					foreach (var ret in GetAllCombinations(src, length, pos))
					{
						yield return ret;
					}
				}
				else
				{
					yield return pos.ToArray();
				}

				pos.Remove(i);
			}
		}

		#endregion

		#region HashSet`1

		/// <summary>
		/// 在HashSet中添加值，并返回是否添加成功
		/// </summary>
		/// <typeparam name="T">HashSet的类型</typeparam>
		/// <param name="hashset">目标HashSet</param>
		/// <param name="obj">要添加的值</param>
		/// <returns>true 表示添加成功；false 表示已经存在</returns>
		public static bool SafeAdd<T>(this HashSet<T> hashset, T obj)
		{
			if (hashset.Contains(obj)) return false;
			hashset.Add(obj);
			return true;
		}

		#endregion

		#region Deconstruct

		public static void Deconstruct<T>(this IEnumerable<T> source, out T first)
		{
			first = source.First();
		}

		public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out T second)
		{
			var args = source.Take(2).ToArray();
			first = args[0];
			second = args[1];
		}

		public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out T second, out T third)
		{
			var args = source.Take(2).ToArray();
			first = args[0];
			second = args[1];
			third = args[2];
		}

		public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out T second, out T third, out T forth)
		{
			var args = source.Take(2).ToArray();
			first = args[0];
			second = args[1];
			third = args[2];
			forth = args[3];
		}

		public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out T second, out T third, out T forth, out T fifth)
		{
			var args = source.Take(2).ToArray();
			first = args[0];
			second = args[1];
			third = args[2];
			forth = args[3];
			fifth = args[4];
		}

		public static void Deconstruct<T>(this IEnumerable<T> source, out T first, out T second, out T third, out T forth, out T fifth, out T sixth)
		{
			var args = source.Take(2).ToArray();
			first = args[0];
			second = args[1];
			third = args[2];
			forth = args[3];
			fifth = args[4];
			sixth = args[5];
		}

		public static void Deconstruct<T>(this T[] args, out T first)
		{
			first = args[0];
		}

		public static void Deconstruct<T>(this T[] args, out T first, out T second)
		{
			first = args[0];
			second = args[1];
		}

		public static void Deconstruct<T>(this T[] args, out T first, out T second, out T third)
		{
			first = args[0];
			second = args[1];
			third = args[2];
		}

		public static void Deconstruct<T>(this T[] args, out T first, out T second, out T third, out T forth)
		{
			first = args[0];
			second = args[1];
			third = args[2];
			forth = args[3];
		}

		public static void Deconstruct<T>(this T[] args, out T first, out T second, out T third, out T forth, out T fifth)
		{
			first = args[0];
			second = args[1];
			third = args[2];
			forth = args[3];
			fifth = args[4];
		}

		public static void Deconstruct<T>(this T[] args, out T first, out T second, out T third, out T forth, out T fifth, out T sixth)
		{
			first = args[0];
			second = args[1];
			third = args[2];
			forth = args[3];
			fifth = args[4];
			sixth = args[5];
		}

		public static void Deconstruct<T>(this IList<T> args, out T first)
		{
			first = args[0];
		}

		public static void Deconstruct<T>(this IList<T> args, out T first, out T second)
		{
			first = args[0];
			second = args[1];
		}

		public static void Deconstruct<T>(this IList<T> args, out T first, out T second, out T third)
		{
			first = args[0];
			second = args[1];
			third = args[2];
		}

		public static void Deconstruct<T>(this IList<T> args, out T first, out T second, out T third, out T forth)
		{
			first = args[0];
			second = args[1];
			third = args[2];
			forth = args[3];
		}

		public static void Deconstruct<T>(this IList<T> args, out T first, out T second, out T third, out T forth, out T fifth)
		{
			first = args[0];
			second = args[1];
			third = args[2];
			forth = args[3];
			fifth = args[4];
		}

		public static void Deconstruct<T>(this IList<T> args, out T first, out T second, out T third, out T forth, out T fifth, out T sixth)
		{
			first = args[0];
			second = args[1];
			third = args[2];
			forth = args[3];
			fifth = args[4];
			sixth = args[5];
		}
		public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, out TKey key, out TValue value)
		{
			key = kvp.Key;
			value = kvp.Value;
		}

		#endregion
	}
}
