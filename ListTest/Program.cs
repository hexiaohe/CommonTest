using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListTest
{
    /// <summary>
    /// List测试项目
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------我是分割线-------------------------------");
        }
    }

    public class Product
    {
        public short Id { get; set; }
        public string Name { get; set; }        
    }

    #region Concat
    /// <summary>
    /// 连接两个序列
    /// </summary>
    public class ListConcatTest
    {
        public void ListConcat() { }
    }
    #endregion

    #region Union
    /// <summary>
    /// 并集，将两个序列链接成一个新的列表，此方法会排除重复项
    /// </summary>
    public class ListUnionTest 
    {
        public void ListUnion() { }
    }
    #endregion

    #region Intersect
    /// <summary>
    /// 交集
    /// </summary>
    public class ListIntersectTest
    {
        public void ListIntersect() { }
    }
    #endregion

    #region Except
    public class ProductEquality : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (x.Id == y.Id && x.Name == y.Name)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Product obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return obj.ToString().GetHashCode();
            }
        }
    }

    /// <summary>
    /// 差集，从一个集合删除存在另一个集合中的项，即集合差
    /// </summary>
    public class ListExceptTest
    {
        public void ListExcept() 
        {
            IList<Product> productList1 = new List<Product> { new Product{Id=1,ClassId = 103,OrgId = 1,TeacherName = "教师1",TargetType = 0},
                                                            new Product{Id=2,ClassId = 103,OrgId = 1,TeacherName = "助教1",TargetType = 1},
                                                            new Product{Id=3,ClassId = 103,OrgId = 1,TeacherName = "教师2",TargetType = 1}
                                                          };
            IList<Product> productList2 = new List<Product> { new Product{Id=1,ClassId = 103,OrgId = 1,TeacherName = "教师1",TargetType = 0},
                                                            new Product{Id=2,ClassId = 103,OrgId = 1,TeacherName = "助教2",TargetType = 1},
                                                            new Product{Id=0,ClassId = 103,OrgId = 1,TeacherName = "助教3",TargetType = 1}
                                                          };


            var deleteSchedule = classTeacherList1.Except(classTeacherList2).ToList();
            int index = 1;
            foreach (ClassTeacherListDto c in deleteSchedule)
            {
                Console.WriteLine("第" + index + "个");
                Console.WriteLine("Id:" + c.Id + " ClassId:" + c.ClassId + " OrgId:" + c.OrgId + " TeacherName:" + c.TeacherName + " TargetType:" + c.TargetType);
                index++;
            }
            Console.WriteLine("------------------------------我是分割线-------------------------------");
            var deleteSchedule2 = classTeacherList2.Except(classTeacherList1, new ClassTeacherListDtoListEquality()).ToList();
            int i = 11;
            foreach (ClassTeacherListDto c in deleteSchedule2)
            {
                Console.WriteLine("第" + i + "个");
                Console.WriteLine("Id:" + c.Id + " ClassId:" + c.ClassId + " OrgId:" + c.OrgId + " TeacherName:" + c.TeacherName + " TargetType:" + c.TargetType);
                i++;
            }

            ProductA[] fruits1 = { new ProductA { Name = "apple", Code = 9 }, 
                       new ProductA { Name = "orange", Code = 4 },
                        new ProductA { Name = "lemon", Code = 12 } };

            ProductA[] fruits2 = { new ProductA { Name = "apple", Code = 9 } };

            //Get all the elements from the first array
            //except for the elements from the second array.

            IEnumerable<ProductA> except =
                fruits1.Except(fruits2);

            foreach (var product in except)
                Console.WriteLine(product.Name + " " + product.Code);
        }
    }
    #endregion

    #region Distinct
    /// <summary>
    /// 去除重复项
    /// </summary>
    public class ListDistinctTest
    {
        public void ListDistinct() { }
    }
    #endregion
}
    