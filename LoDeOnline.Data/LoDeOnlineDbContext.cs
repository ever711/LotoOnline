using LoDeOnline.Data.Mappings;
using LoDeOnline.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Data
{
    public class MyERPDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public MyERPDbContext()
           : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<DaiXoSo> DaiXoSos { get; set; }
        public DbSet<KetQuaXoSo> KetQuaXoSos { get; set; }
        public DbSet<KetQuaXoSoCT> KetQuaXoSoCTs { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ResGroup> ResGroups { get; set; }
        public DbSet<IRModel> IRModels { get; set; }
        public DbSet<IRModelAccess> IRModelAccesses { get; set; }
        public DbSet<IRRule> IRRules { get; set; }
        public DbSet<LoaiDe> LoaiDes { get; set; }
        public DbSet<LoaiDeRule> LoaiDeRules { get; set; }
        public DbSet<LoaiDeCategory> LoaiDeCategories { get; set; }
        public DbSet<LoDeCategory> LoDeCategories { get; set; }
        public DbSet<DanhDe> DanhDes { get; set; }
        public DbSet<IRSequence> IRSequences { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<AccountMove> AccountMoves { get; set; }
        public DbSet<AccountMoveLine> AccountMoveLines { get; set; }
        public DbSet<AccountJournal> AccountJournals { get; set; }
        public DbSet<AccountFullReconcile> AccountFullReconciles { get; set; }
        public DbSet<AccountPartialReconcile> AccountPartialReconciles { get; set; }
        public DbSet<AccountPayment> AccountPayments { get; set; }
        public DbSet<ResBank> ResBanks { get; set; }
        public DbSet<ResPartnerBank> ResPartnerBanks { get; set; }
        public DbSet<IRModelData> IRModelDatas { get; set; }
        public DbSet<DanhDeLine> DanhDeLines { get; set; }
        public DbSet<DanhDeLineXien> DanhDeLineXiens { get; set; }
        public DbSet<TinTuc> TinTucs { get; set; }

        public static MyERPDbContext Create()
        {
            return new MyERPDbContext();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        #region Utilities

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DaiXoSoMap());
            modelBuilder.Configurations.Add(new KetQuaXoSoMap());
            modelBuilder.Configurations.Add(new KetQuaXoSoCTMap());
            modelBuilder.Configurations.Add(new PartnerMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new ApplicationUserMap());
            modelBuilder.Configurations.Add(new ResGroupMap());
            modelBuilder.Configurations.Add(new IRModelMap());
            modelBuilder.Configurations.Add(new IRModelAccessMap());
            modelBuilder.Configurations.Add(new IRRuleMap());
            modelBuilder.Configurations.Add(new LoaiDeRuleMap());
            modelBuilder.Configurations.Add(new LoaiDeMap());
            modelBuilder.Configurations.Add(new LoDeCategoryMap());
            modelBuilder.Configurations.Add(new DanhDeMap());
            modelBuilder.Configurations.Add(new LoaiDeCategoryMap());
            modelBuilder.Configurations.Add(new IRSequenceMap());
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new AccountTypeMap());
            modelBuilder.Configurations.Add(new AccountMoveMap());
            modelBuilder.Configurations.Add(new AccountMoveLineMap());
            modelBuilder.Configurations.Add(new AccountJournalMap());
            modelBuilder.Configurations.Add(new AccountFullReconcileMap());
            modelBuilder.Configurations.Add(new AccountPartialReconcileMap());
            modelBuilder.Configurations.Add(new AccountPaymentMap());
            modelBuilder.Configurations.Add(new ResBankMap());
            modelBuilder.Configurations.Add(new ResPartnerBankMap());
            modelBuilder.Configurations.Add(new IRModelDataMap());
            modelBuilder.Configurations.Add(new DanhDeLineMap());
            modelBuilder.Configurations.Add(new DanhDeLineXienMap());
            modelBuilder.Configurations.Add(new TinTucMap());

            base.OnModelCreating(modelBuilder);
        }


        /// <summary>
        /// Attach an entity to the context or return an already attached entity (if it was already attached)
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : Entity, new()
        {
            //little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
            var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
            if (alreadyAttached == null)
            {
                //attach new entity
                Set<TEntity>().Attach(entity);
                return entity;
            }

            //entity is already loaded
            return alreadyAttached;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create database script
        /// </summary>
        /// <returns>SQL to generate database</returns>
        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Execute stores procedure and load a list of entities at the end
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Entities</returns>
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : Entity, new()
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                }
            }

            var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

            //performance hack applied as described here - http://www.nopcommerce.com/boards/t/25483/fix-very-important-speed-improvement.aspx
            bool acd = this.Configuration.AutoDetectChangesEnabled;
            try
            {
                this.Configuration.AutoDetectChangesEnabled = false;

                for (int i = 0; i < result.Count; i++)
                    result[i] = AttachEntityToContext(result[i]);
            }
            finally
            {
                this.Configuration.AutoDetectChangesEnabled = acd;
            }

            return result;
        }

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }



        #endregion

        //public System.Data.Entity.DbSet<MyERP.Domain.ViewModels.FinancialViewModel> FinancialViewModels { get; set; }
    }
}
