using System;
using System.Collections.Generic;
using Demo.Data;
using Demo.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Demo.App
{
    class Program
    {
        static void Main(string[] args)
        {
#if false
            using var context = new DemoContext();


            #region insert
            var serieA = new League
            {
                Name = "Serie A",
                Country = "Italy"
            };
            var serieB = new League
            {
                Name = "Serie B",
                Country = "Paris"
            };
            //var serieC = context.Leagues.Single(x => x.Name == "Serie C");
            var milan = new Club
            {
                Name = "AC Milan",
                Country = "Milan",
                DateofEstablishment = new DateTime(1997, 10, 26),
                League = serieA
            };
            /*context.Leagues.AddRange(serieA,serieB);
            context.Clubs.AddRange(milan);*/

            //context.Leagues.AddRange(new List<League> { serieA, serieB });
            //context.Leagues.Add(serieA);
            var count = context.SaveChanges(); //这时候才把你做的修改添加到数据库里了,返回的是这个操作所影响的行数
            Console.WriteLine(count);
            #endregion

            #region search

            var leagues = context.Leagues
                .OrderBy(x=>x.Id)
                .Where(x=>x.Country=="Italy") //查询条件 
                .ToList();//只有到这一行，才会执行查询语句

            var info = context.Clubs
                .Where(x => x.Id > 0) //选出club里id>0的列
                .Include(x => x.League) //并且查出列中对应league的详细信息 inculde 就相当于关联，会对其进行追踪，但如果dbset里没有就不会对其进行追踪的，加include也没用
                .Include(x => x.Players)
                    .ThenInclude(y => y.Resume) //查出每个player对应的简历
                .First();

            //查出跟club相关联的league以后还会对其进行追踪，可以修改
            info.League.Name += "000";
            context.SaveChanges();

            var info1 = context.Clubs
                .Where(x => x.Id > 0)
                .Select(x => new
                {
                    x.Id,
                    LeagueName = x.League.Name,
                    x.Name,
                    Players = x.Players.Where(p => p.DateofBirth > new DateTime(1990, 1, 1))
                }).ToList();

            //使用原生sql语句
            var leagues5 = context.Leagues.FromSqlRaw("SELECT * FROM dbo.Leagues").ToList();

            /* var italy = "Italy";
             var leagues1 = context.Leagues
                .Where(x => x.Country == italy) 
                .ToList();*/

           /* Country like "%e%"*/
            /*var leagues1 = context.Leagues
               .Where(x => x.Country.Contains("e"))
               .ToList();

            var leagues2 = context.Leagues
               .Where(x => 
                EF.Functions.Like(x.Country,"%e%"))
               .ToList();*/

            foreach (var league in leagues)
            {
                Console.WriteLine(league.Name);
            }


            #endregion

            #region delete
            //只能删除被追踪的数据，数据只有从数据库里查询出来了才能被追踪
            var milan1 = context.Clubs.Single(x => x.Name == "AC Milan");
            //调用删除方法
            context.Remove(milan1);
            //执行删除动作
            var count1 = context.SaveChanges();
            Console.WriteLine(count1);
            #endregion

            #region update
            var league1 = context.Leagues.First();
            league1.Name += "__";
            var count2 = context.SaveChanges();
            Console.WriteLine(count2);

            var league3 = context.Leagues.AsNoTracking().First();
            league3.Name += "++";
            context.Leagues.Update(league3);//这种方式，除主键外所有的属性都会呈修改状态
            var count3 = context.SaveChanges();
            Console.WriteLine(count3);

            //对于离线数据 要这样update
            var game = context.Games
                .Include(x => x.GamePlayers)
                    .ThenInclude(x => x.Player)
                .First();

            var firstPlayer = game.GamePlayers[0].Player;
            firstPlayer.Name += "$";
            {
                using var newContext = new DemoContext();
                newContext.Players.Update(firstPlayer);//这个方式会把根firstplayer相关联的那些表都更新一遍... 解决方法：
                //newContext.Entry(firstPlayer).State = EntityState.Modified; //只会修改这个一列数据，其他相关联的表格不会改
                newContext.SaveChanges();
            }

        
            #endregion

            #region 表之间的关系
            var serieA1 = context.Leagues.Single(x => x.Name == "Serie A");
            var juventus = new Club
            {
                League = serieA1,
                Name = "Juventus",
                Country = "Torino",
                DateofEstablishment = new DateTime(1897, 11, 1),
                Players = new List<Player>
                {
                    new Player
                    {
                        Name="C.Ronaldo",
                        DateofBirth=new DateTime(1985,2,5)
                    }
                }
            };
            context.Clubs.Add(juventus);
            int count4 = context.SaveChanges();//同时会新增一个club 和一个球员
            Console.WriteLine(count4);

            //在现有俱乐部基础上添加球员
            var juventus1 = context.Clubs.Single(x => x.Name == "Juventus");
            juventus1.Players.Add(new Player
            {
                Name = "Gonzalo Higuain",
                DateofBirth = new DateTime(1987, 12, 10)
            });
            context.SaveChanges();

            //给球员添加简历
            var resume = new Resume
            {
                PlayerId = 1, //设置外键
                Description = "The best player ever"
            };
            context.Resumes.Add(resume);
            context.SaveChanges();

            //多对多的关系
            var gameplayer = new GamePlayer
            {
                PlayerId = 1,
                GameId = 2 //把player和game关联起来了
            };
            context.Add(gameplayer);
            context.SaveChanges();

            #endregion

#endif

        }
    }
}
