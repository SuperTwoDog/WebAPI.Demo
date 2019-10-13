using Entity.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Areas.Mes.Controllers
{
    public class MTaskController : ApiController
    {
        #region 获取数据
        /// <summary>
        /// 获取生产任务单列表
        /// </summary>
        /// <param name="index">增长属性</param>
        /// <remarks>
        /// </remarks>
        [HttpGet]
        [ApiAuthor(Name = "M·Tan", Status = DevStatus.Wait, Time = "2018-08-07")]
        public IEnumerable<ProTaskBillsModel> GetList(int index)
        {
            return null;
        }

        /// <summary>
        /// 获取生产任务单
        /// </summary>
        /// <param name="keyValue">任务单主键或任务单据号</param>
        /// <remarks>
        /// 生产赋码时，可按生产任务单进行赋码<br/>
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "M·Tan", Status = DevStatus.Wait, Time = "2018-07-26")]
        public string GetEntity(string keyValue)
        {
            return "";
        }
        #endregion

        #region 提交数据
        #endregion
    }
}
