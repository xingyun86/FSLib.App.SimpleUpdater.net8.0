namespace FSLib.Extension
{
	using System;
	using System.Threading;

	public abstract class DeferredSource<T>
	{
		protected int _finished;

		public DeferredSource(bool captureContext = true)
		{
			if (captureContext)
				Context = SynchronizationContext.Current;
		}

		/// <summary>
		/// ��û����õ�ǰ��״̬
		/// </summary>
		public SynchronizationContext Context { get; private set; }

		/// <summary>
		/// ��õ�ǰ�Ľ��
		/// </summary>
		public T Result { get; protected set; }

		/// <summary>
		/// ��õ�ǰ�Ĵ���
		/// </summary>
		public Exception Exception { get; protected set; }

		/// <summary>
		/// ע�����ʱӦ�õ��õĻص�
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public DeferredSource<T> Done(Action<object, DeferredEventArgs<T>> action)
		{
			if (action == null)
				throw new ArgumentNullException("action", "action is null.");

			if (_finished == 2)
			{
				if (Context == null) action(this, new DeferredEventArgs<T>(Result, Exception));
				else Context.Post(_ => action(this, new DeferredEventArgs<T>(Result, Exception)), null);
			}
			else OperationSuccess += new EventHandler<DeferredEventArgs<T>>(action);

			return this;
		}

		/// <summary>
		/// ע��ʧ��ʱӦ�õ��õĻص�
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public DeferredSource<T> Fail(Action<object, DeferredEventArgs<T>> action)
		{
			if (action == null)
				throw new ArgumentNullException("action", "action is null.");

			if (_finished == 1)
			{
				if (Context == null) action(this, new DeferredEventArgs<T>(Result, Exception));
				else Context.Post(_ => action(this, new DeferredEventArgs<T>(Result, Exception)), null);
			}
			else OperationFailed += new EventHandler<DeferredEventArgs<T>>(action);

			return this;
		}

		/// <summary>
		/// ע�ᵱ���ȷ����仯ʱ��Ҫ���еĻص�
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public DeferredSource<T> Progress(Action<object, DeferredProgressEventArgs> action)
		{
			if (action == null)
				throw new ArgumentNullException("action", "action is null.");

			ProgressChanged += new EventHandler<DeferredProgressEventArgs>(action);

			return this;
		}

		/// <summary>
		/// ע�����۳ɹ���ʧ�ܶ�����ִ�еĻص�
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public DeferredSource<T> Always(Action<object, DeferredEventArgs<T>> action)
		{
			if (action == null)
				throw new ArgumentNullException("action", "action is null.");

			if (_finished != 0)
			{
				if (Context == null) action(this, new DeferredEventArgs<T>(Result, Exception));
				else Context.Post(_ => action(this, new DeferredEventArgs<T>(Result, Exception)), null);
			}
			else OperationCompleted += new EventHandler<DeferredEventArgs<T>>(action);

			return this;
		}

		#region �������

		/// <summary>
		/// ���Ϊ����ʧ��
		/// </summary>
		/// <param name="ex">������쳣</param>
		/// <param name="result">���õĽ��</param>
		protected void Reject(Exception ex = null, T result = default(T))
		{
			if (Interlocked.Exchange(ref _finished, 1) != 0)
				return;

			Result = result;
			Exception = ex;
			OnOperationFailed(new DeferredEventArgs<T>(Result, Exception));
		}

		/// <summary>
		/// ���Ϊ�����
		/// </summary>
		/// <param name="result">����Ľ��</param>
		protected void Resolve(T result)
		{
			if (Interlocked.Exchange(ref _finished, 2) != 0)
				return;

			Result = result;
			Exception = null;
			OnOperationSuccess(new DeferredEventArgs<T>(Result, Exception));
		}

		/// <summary>
		/// ֪ͨ���ȱ仯
		/// </summary>
		/// <param name="progressObject"></param>
		protected void Notify(object progressObject)
		{
			OnProgressChanged(new DeferredProgressEventArgs(progressObject));
		}

		#endregion

		#region �¼�

		void TriggerHandler(EventHandler handler)
		{
			if (handler == null)
				return;

			if (Context == null)
				handler(this, EventArgs.Empty);
			else Context.Post(() => handler(this, EventArgs.Empty));
		}
		void TriggerHandler<T>(EventHandler<T> handler, T evargs) where T : EventArgs
		{
			if (handler == null)
				return;

			if (Context == null)
				handler(this, evargs);
			else Context.Post(() => handler(this, evargs));
		}

		public event EventHandler BeforeStart;

		/// <summary>
		/// ���� <see cref="BeforeStart" /> �¼�
		/// </summary>
		protected virtual void OnBeforeStart()
		{
			TriggerHandler(BeforeStart);
		}


		public event EventHandler<DeferredEventArgs<T>> OperationSuccess;

		/// <summary>
		/// ���� <see cref="OperationSuccess" /> �¼�
		/// </summary>
		/// <param name="ea">�������¼��Ĳ���</param>
		protected virtual void OnOperationSuccess(DeferredEventArgs<T> ea)
		{
			TriggerHandler(OperationSuccess, ea);
			OnOperationCompleted(ea);
		}

		public event EventHandler<DeferredEventArgs<T>> OperationFailed;

		/// <summary>
		/// ���� <see cref="OperationFailed" /> �¼�
		/// </summary>
		/// <param name="ea">�������¼��Ĳ���</param>
		protected virtual void OnOperationFailed(DeferredEventArgs<T> ea)
		{
			TriggerHandler(OperationFailed, ea);
			OnOperationCompleted(ea);
		}

		public event EventHandler<DeferredEventArgs<T>> OperationCompleted;

		/// <summary>
		/// ���� <see cref="OperationCompleted" /> �¼�
		/// </summary>
		/// <param name="ea">�������¼��Ĳ���</param>
		protected virtual void OnOperationCompleted(DeferredEventArgs<T> ea)
		{
			TriggerHandler(OperationCompleted, ea);
		}

		/// <summary>
		/// ���ȷ����仯
		/// </summary>
		public event EventHandler<DeferredProgressEventArgs> ProgressChanged;

		/// <summary>
		/// ���� <see cref="ProgressChanged" /> �¼�
		/// </summary>
		/// <param name="ea">�������¼��Ĳ���</param>
		protected virtual void OnProgressChanged(DeferredProgressEventArgs ea)
		{
			TriggerHandler(ProgressChanged, ea);
		}

		#endregion

	}
}