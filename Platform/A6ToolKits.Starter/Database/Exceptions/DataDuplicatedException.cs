using A6ToolKits.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A6ToolKits.Database.Exceptions
{
    /// <summary>
    /// 表示数据重复的异常。
    /// </summary>
    public class DataDuplicatedException : FrameworkExceptionBase
    {
        /// <summary>
        /// 初始化 <see cref="DataDuplicatedException"/> 类的新实例。
        /// </summary>
        /// <param name="information">错误信息。</param>
        /// <param name="details">错误详情。</param>
        public DataDuplicatedException(string information, string? details = null) 
            : base(ErrorCode.RuntimeError, information, details)
        {
        }
    }
}
