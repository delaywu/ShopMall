using System;
using System.Collections.Generic;

namespace ShopMall.Site.Infrastructure.DapperExtensions
{
    public class PagerData<TModel>
    {
        public PagerData()
        {
        }
        public PagerData(int pageIndex, int pageSize)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
        }
        int _pageindex = 1;
        int _pagesize = 10;
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
                else
                {
                    return 10;
                }
            }
            set
            {
                _pagesize = value;
            }
        }
        /// <summary>
        /// 分页索引
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (_pageindex > 0)
                {
                    return _pageindex;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _pageindex = value;
            }
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (RecordCount == 0)
                {
                    return 0;
                }
                else
                {
                    return (int)Math.Ceiling((double)RecordCount / (double)PageSize);
                }
            }
        }
        /// <summary>
        /// 分页数据
        /// </summary>
        public IEnumerable<TModel> Data { get; set; }
    }
}
