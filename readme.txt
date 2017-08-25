using Supplier.Dal;
using Sypplier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supplier.Bll
{
    /// <summary>
    /// 省市区信息管理
    /// </summary>
    public class AreasBll
    {
        private AreasDal dal = new AreasDal();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<province>> GetProvinceAsyn(string where, params object[] args)
        {
            return System.Threading.Tasks.Task.Run(() => { return new AreasDal().GetProvince(where,args); });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<city>> GetCityAsyn(string where, params object[] args)
        {
            return System.Threading.Tasks.Task.Run(() => { return new AreasDal().GetCity(where, args); });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<area>> GetAreaAsyn(string where, params object[] args)
        {
            return System.Threading.Tasks.Task.Run(() => { return new AreasDal().GetArea(where, args); });
        }

        public Task<province> GetSingleProvinceAsyn(string code)
        {
            return System.Threading.Tasks.Task.Run(() => { return new AreasDal().GetSingleProvince(code); });
        }

        public Task<city> GetSingleCityAsyn(string code)
        {
            return System.Threading.Tasks.Task.Run(() => { return new AreasDal().GetSingleCity(code); });
        }

        public Task<area> GetSingleAreaAsyn(string code)
        {
            return System.Threading.Tasks.Task.Run(() => { return new AreasDal().GetSingleArea(code); });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<province>> GetProvinceNoCacheAsyn(string where, params object[] args)
        {
            return System.Threading.Tasks.Task.Run(() => { return new AreasDal().GetProvinceNoCache(where, args); });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<city>> GetCityNoCacheAsyn(string where, params object[] args)
        {
            return System.Threading.Tasks.Task.Run(() => { return new AreasDal().GetCityNoCache(where, args); });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<area>> GetAreaNoCacheAsyn(string where, params object[] args)
        {
            return System.Threading.Tasks.Task.Run(() => { return new AreasDal().GetAreaNoCache(where, args); });
        }

    }
}
