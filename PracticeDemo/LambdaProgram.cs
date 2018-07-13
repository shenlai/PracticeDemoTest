using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PracticeDemo
{
    public class ParameterReplacer : ExpressionVisitor
    {
        public ParameterExpression parameterExpression { get; set; }
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.parameterExpression = paramExpr;
        }
        public Expression Replace(Expression exp)
        {
            return this.Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return this.parameterExpression;
        }
    }
    class LambdaProgram
    {
        /*http://www.cnblogs.com/jesse2013/p/happylambda.html*/
        /*
         * 匿名方法
         * 语句Lambda
         * 表达式Lambda
         * lambda表达式的内部机制（反编译）
         * 表达式树
         */

        public delegate int DelegateTest(int n1, int n2);

        private static MemberInfo GetMemberInfo<T, TKey>(Expression<Func<T, TKey>> e) where T : class
        {
            if (e == null)
            {
                return null;
            }
            MemberExpression mem = e.Body as MemberExpression;
            if (mem != null)
            {
                return mem.Member;
            }
            return null;
        }
        static void Main(string[] args)
        {

            ////x=>x>5和 x=>x<10  和并成一个表达式树：x=>x>5 && x<10
            Expression<Func<int, bool>> exp1 = x => x > 5;
            Expression<Func<int, bool>> exp2 = x => x < 10;
            ParameterExpression y = Expression.Parameter(typeof(int), "y");
            var newExp = new NewExpression(y);
            var newexp1 = newExp.Replace(exp1.Body);
            var newexp2 = newExp.Replace(exp2.Body);
            var newbody = Expression.And(newexp1, newexp2);
            //创建表达式树
            Expression<Func<int, bool>> res = Expression.Lambda<Func<int, bool>>(newbody, y);
            //将表达式树描述的lambda表达式编译为可执行代码，并生成表示该lambda表达式的委托
            Func<int, bool> del = res.Compile();
            Console.WriteLine(del(7));
            //Console.ReadLine();


            #region  Order 表达式树合并

            Expression<Func<Order, Order>> evaluator1 = ordr => new Order() { price = 1m };
            
            Expression<Func<Order, Order>> evaluator2 = ordr => new Order() { version = ordr.version + 1 };

            // evaluator1+evaluator1 ==>Expression<Func<Order, Order>> evaluator2 = ordr => new Order() { price = 1m; version = ordr.version + 1 };

            ParameterExpression o = Expression.Parameter(typeof(Order), "o");
            var newExpkk = new NewExpression(o);
            var evaluator1Body = newExpkk.Replace(evaluator1.Body);
            var evaluator2Body = newExpkk.Replace(evaluator2.Body);

            MemberInitExpression mem1 = evaluator1Body as MemberInitExpression;
            MemberInitExpression mem2 = evaluator2Body as MemberInitExpression;
            //var constructor = mem1.NewExpression.Constructor;
            //var orderNewExpression = Expression.New(constructor, mem1.NewExpression.Arguments);

            List<MemberAssignment> list = new List<MemberAssignment>();
            list.AddRange(mem1.Bindings.Cast<MemberAssignment>());
            list.AddRange(mem2.Bindings.Cast<MemberAssignment>());
            //var init = Expression.MemberInit(orderNewExpression, list.ToArray());
            var init = Expression.MemberInit(mem1.NewExpression, list.ToArray());

            var evaluator = Expression.Lambda<Func<Order, Order>>(init, o);

            Func<Order, Order> newOrder = evaluator.Compile();
            var order = newOrder(new Order());


            //MemberInitExpression init = evaluator.Body as MemberInitExpression;
            //List<MemberAssignment> list = new List<MemberAssignment>();
            //list.AddRange(init.Bindings.Cast<MemberAssignment>());

            //MemberInfo mi = null;
            //MemberAssignment ass = null;

            //if (!(init.Bindings.ToList().Exists(i => i.Member.Name == "LastUpdateOperator")))
            //{
            //    mi = GetMemberInfo<ITraceDataEntityBase, string>(p => p.LastUpdateOperator);
            //    ass = Expression.Bind(mi, Expression.Constant(operater.Name));
            //    list.Add(ass);
            //}

            //if (!(init.Bindings.ToList().Exists(i => i.Member.Name == "LastUpdateTime")))
            //{
            //    mi = GetMemberInfo<ITraceDataEntityBase, DateTime>(p => p.LastUpdateTime);
            //    ass = Expression.Bind(mi, Expression.Constant(DateTime.Now));
            //    list.Add(ass);
            //}

            //init = Expression.MemberInit(init.NewExpression, list.ToArray());

            //evaluator = Expression.Lambda<Func<Order, Order>>(init, Expression.Parameter(typeof(Order)));

            //var fun = ordertree.Compile();

            return;

            #endregion


            var xx = 0.5;
            var yy = Math.Round(xx, MidpointRounding.AwayFromZero);
            #region
            //创建表达式树：Expression<Func<int, int>> exp = x => x + 1;
            ParameterExpression param = Expression.Parameter(typeof(int), "x");
            ConstantExpression value = Expression.Constant(1, typeof(int));
            BinaryExpression body = Expression.Add(param, value);
            Expression<Func<int, int>> lambdatree = Expression.Lambda<Func<int, int>>(body, param);
            Console.WriteLine("参数param：{0}", param);
            Console.WriteLine("描述body：{0}", body);
            Console.WriteLine("表达式树：{0}", lambdatree);

            //解析表达式树：
            //取得表达式树的参数
            ParameterExpression dparam = lambdatree.Parameters[0] as ParameterExpression;
            //取得表达式树描述
            BinaryExpression dbody = lambdatree.Body as BinaryExpression;
            //取得节点
            ParameterExpression left = dbody.Left as ParameterExpression;
            ConstantExpression right = body.Right as ConstantExpression;
            Console.WriteLine("解析出的表达式：{0}=>{1} {2} {3}", param.Name, left.Name, body.NodeType, right.Value);

            #endregion

            ////x=>x>5和 x=>x<10  和并成一个表达式树：x=>x>5 && x<10
            //Expression<Func<int, bool>> exp1 = x => x > 5;
            //Expression<Func<int, bool>> exp2 = x => x < 10;

            ////var newbody1 = Expression.Add(exp1, exp2);
            ////Expression<Func<int, bool>> res2 = Expression.Lambda<Func<int, bool>>(newbody1, exp1.Parameters[0] as ParameterExpression);

            //ParameterExpression y = Expression.Parameter(typeof(int), "y");
            //var newExp = new NewExpression(y);
            //var newexp1 = newExp.Replace(exp1.Body);
            //var newexp2 = newExp.Replace(exp2.Body);
            //var newbody = Expression.And(newexp1, newexp2);
            ////创建表达式树
            //Expression<Func<int, bool>> res = Expression.Lambda<Func<int, bool>>(newbody, y);
            ////将表达式树描述的lambda表达式编译为可执行代码，并生成表示该lambda表达式的委托
            //Func<int, bool> del = res.Compile();
            //Console.WriteLine(del(7));
            //Console.ReadLine();
        }



        public static int Result(int a, int b, DelegateTest @delegate)
        {
            return @delegate(a, b);
        }
        public static int Sum(int a, int b)
        {
            return a + b;
        }


        /*
         * 表达式 表达式树
         * 表达式在CIL中被编译成一个委托， 表达式树被编译成System.Linq.Expressions.Expression类型的数据结构
         */


    }


    public class Order
    {
      public  int version { get; set; }

        public string Name { get; set; }
        public decimal price { get; set; }
    }
}
