namespace FSLib.Extension
{
	using System;

	/// <summary>
	/// �����¼�����
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DeferredProgressEventArgs : EventArgs
	{
		/// <summary>
		/// ��ý��ȱ�ʾ
		/// </summary>
		public object ProgressData { get; private set; }

		/// <summary>
		/// ���� <see cref="DeferredProgressEventArgs{T}" />  ����ʵ��(DeferredProgressEventArgs)
		/// </summary>
		/// <param name="progressData"></param>
		public DeferredProgressEventArgs(object progressData)
		{
			ProgressData = progressData;
		}
	}
}