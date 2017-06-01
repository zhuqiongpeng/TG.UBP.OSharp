using OSharp.Demo.Models.Identity;
using System;
using OSharp.Data.Entity;

namespace OSharp.Demo.ModelConfigurations_Oracle.Identity
{
    public class UserExtendConfiguration_Oracle : EntityConfigurationBase<UserExtend, Int32>
    {
        public UserExtendConfiguration_Oracle()
        {
            //将实体映射到Oracle数据库
            //Property(t => t.Id).HasColumnName("ID");
            //Property(t => t.RegistedIp).HasColumnName("REGISTEDIP");

            //ToTable("USEREXTENDS");

            HasRequired(m => m.User).WithRequiredDependent(n => n.Extend);
            //HasRequired(m => m.User).WithRequiredDependent(n => n.Extend)
            //    .Map(p =>
            //    {
            //        p.ToTable("USERS");
            //    });
        }
    }
}
