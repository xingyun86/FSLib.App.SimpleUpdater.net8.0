using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSLib.App.SimpleUpdater.Tests
{
	using System.Diagnostics;

	[TestClass]
	public class UpdaterContextTest
	{
		[TestMethod]
		public void UrlInitTest()
		{
			var context = new FSLib.App.SimpleUpdater.Defination.UpdateContext();

			//����������ָ��ģ����ļ���
			context.UpdateDownloadUrl = @"c:\{0}";
			context.UpdateInfoFileName = "update.xml";
			context.Init();

			Assert.AreEqual("update.xml", context.UpdateInfoFileName);
			Assert.AreEqual(@"c:\{0}", context.UpdateDownloadUrl);
			
			//����������ָ������·��
			context.UpdateDownloadUrl = @"c:\update.xml";
			context.UpdateInfoFileName = null;
			context.Init();
			Assert.AreEqual("update.xml", context.UpdateInfoFileName);
			Assert.AreEqual(@"c:\{0}", context.UpdateDownloadUrl);

			//�������� - ָֻ��ģ�壬δָ���ļ�
			context.UpdateDownloadUrl = @"c:\{0}";
			context.UpdateInfoFileName = null;
			context.Init();

			Assert.AreEqual("update_c.xml", context.UpdateInfoFileName);
			Assert.AreEqual(@"c:\{0}", context.UpdateDownloadUrl);

			//�������� - ָֻ��Ŀ¼
			context.UpdateDownloadUrl = @"c:\";
			context.UpdateInfoFileName = null;
			context.Init();
			Assert.AreEqual("update_c.xml", context.UpdateInfoFileName);
			Assert.AreEqual(@"c:\{0}", context.UpdateDownloadUrl);
			
			//����������ָ������ģ����ļ���
			context.UpdateDownloadUrl = "http://localhost/{0}?query";
			context.UpdateInfoFileName = "update.xml";
			context.Init();
			Assert.AreEqual("update.xml", context.UpdateInfoFileName);
			Assert.AreEqual("http://localhost/{0}?query", context.UpdateDownloadUrl);


			//����������ָֻ������xml�ļ�
			context.UpdateDownloadUrl = "http://localhost/update.xml?query";
			context.UpdateInfoFileName = null;
			context.Init();
			Assert.AreEqual("update.xml", context.UpdateInfoFileName);
			Assert.AreEqual("http://localhost/{0}?query", context.UpdateDownloadUrl);


			//����������ָֻ��ģ��
			context.UpdateDownloadUrl = "http://localhost/{0}?query";
			context.UpdateInfoFileName = null;
			context.Init();
			Assert.AreEqual("update_c.xml", context.UpdateInfoFileName);
			Assert.AreEqual("http://localhost/{0}?query", context.UpdateDownloadUrl);


			//����������ָֻ��Ŀ¼
			context.UpdateDownloadUrl = "http://localhost/?query";
			context.UpdateInfoFileName = null;
			context.Init();
			Assert.AreEqual("update_c.xml", context.UpdateInfoFileName);
			Assert.AreEqual("http://localhost/{0}?query", context.UpdateDownloadUrl);

		}
	}
}
