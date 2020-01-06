using FXKJ.Infrastructure.Entities.QueryModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FXKJ.Infrastructure.Core.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchFilterUtil
    {
        /// <summary>
        /// 拼接数据sql
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static string ConvertFilters(List<ConditionItem> conditions)
        {
            var searchCase = string.Empty;
            if (conditions.Count() > 0)
            {
                string group = " ";
                foreach (var rule in conditions)
                {
                    group += rule.Operator+" ";
                    //过滤输入的数据
                    string data = StripSqlInjection(rule.Value.ToString());
                    switch (rule.Method)
                    {
                        case QueryMethod.Equal: //等于
                            searchCase += group + rule.Field + " ='" + data + "'";
                            break;
                        case QueryMethod.NotEqual: //不等于
                            searchCase += group + rule.Field + " !='" + data + "'";
                            break;
                        case QueryMethod.StartsWith: //以...开始
                            searchCase += group + rule.Field + " like '" + data + "%'";
                            break;
                        case QueryMethod.NotStartsWith: //不以...开始
                            searchCase += group + rule.Field + " not like '" + data + "%'";
                            break;
                        case QueryMethod.EndsWith: //结束于
                            searchCase += group + rule.Field + " like '%" + data + "'";
                            break;
                        case QueryMethod.NotEndsWith: //不结束于
                            searchCase += group + rule.Field + " not like '%" + data + "'";
                            break;
                        case QueryMethod.LessThan: //小于
                            searchCase += group + rule.Field + " <'" + data + "'";
                            break;
                        case QueryMethod.LessThanOrEqual: //小于等于
                            searchCase += group + rule.Field + " <='" + data + "'";
                            break;
                        case QueryMethod.GreaterThan: //大于
                            searchCase += group + rule.Field + " >'" + data + "'";
                            break;
                        case QueryMethod.GreaterThanOrEqual: //大于等于
                            searchCase += group + rule.Field + " >='" + data + "'";
                            break;
                        case QueryMethod.In: //包括
                            searchCase += group + rule.Field + " in ('" + data + "')";
                            break;
                        case QueryMethod.NotIn: //不包含
                            searchCase += group + rule.Field + " not in ('" + data + "')";
                            break;
                        case QueryMethod.Contains://包含
                            searchCase += group + rule.Field + " like '%" + data + "%'";
                            break;
                        case QueryMethod.NotContains://不包含
                            searchCase += group + rule.Field + " not like '%" + data + "%'";
                            break;
                        case QueryMethod.Null://空值
                            searchCase += group + rule.Field + " =null";
                            break;
                        case QueryMethod.NotNull://非空值
                            searchCase += group + rule.Field + " !=null";
                            break;
                        case QueryMethod.Time://针对时间特别处理
                            searchCase += group + rule.Field + " between '" + data + " 00:00:00' AND '" + data + " 23:59:59'";
                            break;
                    }
                }
            }
            return searchCase;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryOrders"></param>
        /// <returns></returns>
        public static string ConvertOrderBy(IList<QueryOrder> queryOrders)
        {
            string mes = "";
            foreach (var item in queryOrders)
            {
                var sortType = item.IsDesc == true ? "desc" : "asc";
                mes += item.Field + " " + sortType + ",";
            }
            return mes.Substring(0, mes.Length - 1);
        }

        /// <summary>
        /// 替换SQL注入特殊字符
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string StripSqlInjection(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                //过滤 ' --
                const string pattern1 = @"(\%27)|(\')|(\-\-)";
                //防止执行 ' or
                const string pattern2 = @"((\%27)|(\'))\s*((\%6F)|o|(\%4F))((\%72)|r|(\%52))";
                //防止执行sql server 内部存储过程或扩展存储过程
                const string pattern3 = @"\s+exec(\s|\+)+(s|x)p\w+";
                sql = Regex.Replace(sql, pattern1, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, pattern2, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, pattern3, string.Empty, RegexOptions.IgnoreCase);
            }
            return sql;
        }
    }
}
