﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FSLib.Extension.FishLib {
    using System;
    using System.Reflection;


    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SR {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SR() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FSLib.Extension.FishLib.SR", typeof(SR).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 前天 的本地化字符串。
        /// </summary>
        internal static string FriendlyTime_DayBeforeYesterday {
            get {
                return ResourceManager.GetString("FriendlyTime_DayBeforeYesterday", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 {0} 小时前 的本地化字符串。
        /// </summary>
        internal static string FriendlyTime_Hour {
            get {
                return ResourceManager.GetString("FriendlyTime_Hour", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 {0} 分钟前 的本地化字符串。
        /// </summary>
        internal static string FriendlyTime_Minute {
            get {
                return ResourceManager.GetString("FriendlyTime_Minute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 {0} 秒前 的本地化字符串。
        /// </summary>
        internal static string FriendlyTime_Second {
            get {
                return ResourceManager.GetString("FriendlyTime_Second", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 昨天 的本地化字符串。
        /// </summary>
        internal static string FriendlyTime_Yesterday {
            get {
                return ResourceManager.GetString("FriendlyTime_Yesterday", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 字节 的本地化字符串。
        /// </summary>
        internal static string Size_Bytes {
            get {
                return ResourceManager.GetString("Size_Bytes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 GB 的本地化字符串。
        /// </summary>
        internal static string Size_GB {
            get {
                return ResourceManager.GetString("Size_GB", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 KB 的本地化字符串。
        /// </summary>
        internal static string Size_KB {
            get {
                return ResourceManager.GetString("Size_KB", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 MB 的本地化字符串。
        /// </summary>
        internal static string Size_MB {
            get {
                return ResourceManager.GetString("Size_MB", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 TB 的本地化字符串。
        /// </summary>
        internal static string Size_TB {
            get {
                return ResourceManager.GetString("Size_TB", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 2 的本地化字符串。
        /// </summary>
        internal static string String1 {
            get {
                return ResourceManager.GetString("String1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 截取长度过短，不应该再存在省略字符串 的本地化字符串。
        /// </summary>
        internal static string StringExtract_GetSubString_LengthError {
            get {
                return ResourceManager.GetString("StringExtract_GetSubString_LengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 当前任务正在取消，无法更改状态 的本地化字符串。
        /// </summary>
        internal static string TaskAlreadyCancelPending {
            get {
                return ResourceManager.GetString("TaskAlreadyCancelPending", resourceCulture);
            }
        }
    }
}
