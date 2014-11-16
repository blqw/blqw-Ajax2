using System;
using System.Collections.Generic;
using System.Text;

namespace blqw
{
    /// <summary> 用于输出到js中的pager对象
    /// </summary>
    public class Pager
    {
        public Pager()
            :this("pager")
        {

        }

        public Pager(string name)
        {
            Name = name;
        }

        #region 字段
        private int _pageindex;
        private int _pagenumber;
        private int _pagecount;
        private int _itemcount;
        private int _pagesize; 
        #endregion

        /// <summary> js中变量的名称,默认: pager
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary> 当前页索引 从0开始,随pagenumber变化
        /// </summary>
        public int pageindex
        {
            get
            {
                return Math.Min(_pageindex, _pagecount - 1);
            }
            set
            {
                _pageindex = value;
                _pagenumber = value + 1;
                Calc();
            }
        }

        /// <summary> 当前页数 从1开始,随pageindex变化
        /// </summary>
        public int pagenumber
        {
            get
            {
                return Math.Min(_pagenumber, _pagecount);
            }
            set
            {
                _pagenumber = value;
                _pageindex = value - 1;
                Calc();
            }
        }

        /// <summary> 每页数量
        /// </summary>
        public int pagesize
        {
            get
            {
                return _pagesize;
            }
            set
            {
                _pagesize = value;
                Calc();
            }
        }

        /// <summary> 总页数,随itemcount变化
        /// </summary>
        public int pagecount
        {
            get
            {
                return _pagecount;
            }
            set
            {
                _pagecount = value;
                _itemcount = -1;
                Calc();
            }
        }

        /// <summary> 数据总条数,改变pagecount后该值为-1
        /// </summary>
        public int itemcount
        {
            get
            {
                return _itemcount;
            }
            set
            {
                _itemcount = value;
                Calc();
            }
        }
        /// <summary> 计算
        /// </summary>
        private void Calc()
        {
            if (_pageindex < 0)
            {
                _pageindex = 0;
                _pagenumber = 1;
            }
            if (_pagesize > 0)
            {
                if (_itemcount != -1)
                {
                    _pagecount = (_itemcount + _pagesize - 1) / _pagesize;
                }
            }
        }
    }
}
