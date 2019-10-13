using Entity.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebAPI.Areas.Bms.Controllers
{
    /// <summary>
    /// 产品数据API
    /// </summary>
    public class ProductController : ApiController
    {
        #region 获取数据

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <remarks>
        /// 用于同步数据至手持设备<br/>
        /// </remarks>
        [HttpGet]
        [ApiAuthor(Name = "M·Tan", Status = DevStatus.Wait, Time = "2019-09-25")]
        [AllowAnonymous]
        public IEnumerable<MaterielModel> GetList(int index)
        {
            return new List<MaterielModel>();
        }

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="keyValue">主键值或产品编码</param>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "M·Tan", Status = DevStatus.Dev, Time = "2019-09-25")]
        public string GetEntity(string keyValue)
        {
            return "";
        }

        /// <summary>
        /// 获取产品分页列表
        /// </summary>
        /// <param name="page">当前分页</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "M·Tan", Status = DevStatus.Finish, Time = "2019-08-01")]
        public string GetPageList(int page, int pageSize)
        {
            return "";
        }

        #endregion

    }
}
