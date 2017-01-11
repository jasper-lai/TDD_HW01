using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Library.Models
{

    /// <summary>
    /// 用以將原來的 List (例如: List<SaleModel> 作分組之用 )
    /// </summary>
    /// <typeparam name="T">Model 型別</typeparam>
    public class MyGroup<T>
    {
        public int GroupSeq { get; set; }
        public List<T> Datas { get; set; }
    }

}
