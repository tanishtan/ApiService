using AuthenticationApi.DataAccess;
using AuthenticationClassLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace AuthenticationApi.Infrastructure
{
    public interface IAccountsRepository<TEntity, TIdentity>
    {
        IEnumerable<TEntity> GetAllAccountsByCustomerID(int CustomerID);
        TEntity GetAccountByAccountID(TIdentity id);
        void CreateAccount(TEntity entity);
        void DeleteAccountByAccountId(TIdentity id);
    }
    public class AccountsRepository : BaseDataAccess, IAccountsRepository<Account, long>
    {
        public AccountsRepository(IConfiguration config) : base(config)
        {

        }
        public void CreateAccount(Account entity)
        {
            var sqlText1 = "sp_AddAccount";   //stored procedure name
            try
            {
                ExecuteNonQuery(sqlText1, commandType: CommandType.StoredProcedure, new SqlParameter("@Balance", entity.Balance),
                    new SqlParameter("@HasCheckBook", entity.hasCheque),
                    new SqlParameter("@WdQuota", entity.wd_quota),
                    new SqlParameter("@DpQuota", entity.dp_quota),
                    new SqlParameter("@IsActive", entity.isActive),
                    new SqlParameter("@CustId", entity.CustomerID),
                    new SqlParameter("@TypeId", entity.type_id),
                    new SqlParameter("@BranchId", entity.BranchID)
                );
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void DeleteAccountByAccountId(long id)
        {
            var sqlText1 = "sp_DeleteAccountSafely";   //stored procedure name
            try
            {
                ExecuteNonQuery(sqlText1, commandType: CommandType.StoredProcedure, new SqlParameter("@AccountID", id)
                );
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public Account GetAccountByAccountID(long id)
        {
            Account account = new Account();
            var sqlText1 = "sp_GetCustomerAccount";   //stored procedure name
            try
            {
                using (SqlDataReader reader = ExecuteReader(sqlText1, CommandType.StoredProcedure,
            new SqlParameter("@AccountId", id)))
                {
                    // Check if the reader has any rows
                    if (reader.Read())
                    {
                        // Populate account object
                        account.AccountID = (long)reader["AccountId"];
                        account.Balance = (decimal)reader["Balance"];
                        account.hasCheque = (bool)reader["HasCheckBook"];
                        account.wd_quota = (int)reader["WdQuota"];
                        account.dp_quota = (int)reader["DpQuota"];
                        account.isActive = (bool)reader["IsActive"];
                        account.CustomerID = (int)reader["CustId"];
                        account.type_id = (int)reader["TypeId"];
                        account.BranchID = (string)reader["BranchId"];
                    }
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return account;
        }

        public IEnumerable<Account> GetAllAccountsByCustomerID(int CustomerID)
        {
            List<Account> accountsList = new List<Account>();
            var sqlText1 = "sp_GetCustomerAccounts";   //stored procedure name
            try
            {
                using (SqlDataReader reader = ExecuteReader(sqlText1, CommandType.StoredProcedure, new SqlParameter("@CustomerId", CustomerID)))
                {
                    // Check if the reader has any rows
                    if (reader.HasRows)
                    {
                        // Read each row and populate Account objects
                        while (reader.Read())
                        {
                            Account account = new Account();
                            account.AccountID = (long)reader["AccountId"];
                            account.Balance = (decimal)reader["Balance"];
                            account.hasCheque = (bool)reader["HasCheckBook"];
                            account.wd_quota = (int)reader["WdQuota"];
                            account.dp_quota = (int)reader["DpQuota"];
                            account.isActive = (bool)reader["IsActive"];
                            account.CustomerID = (int)reader["CustId"];
                            account.type_id = (int)reader["TypeId"];
                            account.BranchID = (string)reader["BranchId"];

                            accountsList.Add(account);
                        }
                    }
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return accountsList;
        }
    }
}
