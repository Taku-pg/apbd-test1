using apbd_test1.Model;
using Microsoft.Data.SqlClient;

namespace apbd_test1.Repository;

public class VisitRepository : IVisitRepository
{
    private readonly string _connectionString;

    public VisitRepository(IConfiguration _configuration)
    {
        _connectionString = _configuration.GetConnectionString("apbd");
    }
    
    public async Task<bool> DoesVisitExist(int visitId)
    {
        string sql="select 1 from Visit_Service where Visit_Id=@VisitId";
        
        using(SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@VisitId", visitId);
            var res=await cmd.ExecuteScalarAsync();
            return res!=null;
        }
    }

    public async Task<bool> DoesClientExist(int clientId)
    {
        string sql="select 1 from Client where Cleint_Id=@ClientId";
        
        using(SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@ClientId", clientId);
            var res=await cmd.ExecuteScalarAsync();
            return res!=null;
        }
    }

    public async Task<int> DoesMechanicExist(string licenceNumber)
    {
        string sql="select mechanic_id from Mechanic where Licence_number=@LicenceNumber";
        
        using(SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@LicenceNumber", licenceNumber);
            var res=await cmd.ExecuteScalarAsync();
            if (res == null)
                return 0;
            return Convert.ToInt32(res);
        }
    }
    
    public async Task<bool> DoesServiceExist(string serviceName)
    {
        string sql="select 1 from Service where Name=@ServiceName";
        
        using(SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@ServiceName", serviceName);
            var res=await cmd.ExecuteScalarAsync();
            return res!=null;
        }
    }

    public async Task<VisitDTO> GetVisit(int visitId)
    {
        string sql=@"select [date],
            c.first_name AS clientF,c.last_name AS clientL,date_of_birth,
            m.mechanic_id,licence_number,
            [name],base_fee 
            FROM Visit v 
            JOIN Client c ON v.client_id=c.client_id 
            JOIN Mechanic m ON m.mechanic_id=v.mechanic_id 
            JOIN Visit_Service vs ON v.visit_id=vs.visit_id 
            JOIN Service s ON vs.service_id=s.service_id where v.Visit_Id=@VisitId";
        
        VisitDTO visit = null;
        
        using(SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@VisitId", visitId);
            
            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            
            while (await reader.ReadAsync())
            {
                if (visit == null)
                {
                    visit = new VisitDTO()
                    {
                        Date = reader.GetDateTime(reader.GetOrdinal("date")),
                        Client = new ClientDTO()
                        {
                            FirstName = reader.GetString(reader.GetOrdinal("clientF")),
                            LastName = reader.GetString(reader.GetOrdinal("clientL")),
                            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("date_of_birth"))
                        },
                        Mechanic = new MechanicDTO()
                        {
                            MechanicID = reader.GetInt32(reader.GetOrdinal("mechanic_id")),
                            LicenceNumber = reader.GetString(reader.GetOrdinal("licence_number"))
                        }
                    };
                    visit.VisitServices.Add(new VisitServiceDTO()
                    {
                        Name = reader.GetString(reader.GetOrdinal("name")),
                        ServicePrice = reader.GetDecimal(reader.GetOrdinal("base_fee"))
                    });
                }
            }
        }

        return visit;
    }


    public async Task<int> AddVisit(NewVisitDTO newVisit,int mechanicId)
    {
        string sql=@"INSERT INTO Visit(visit_id,client_id,mechanic_id,date) 
                        VALUES (@VisitId,@ClientId,@MechanicId,@Date);
                        SELECT SCOPE_IDENTITY();";
        
        
        using(SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@VisitId", newVisit.VisitId);
            cmd.Parameters.AddWithValue("@ClientId", newVisit.ClientId);
            cmd.Parameters.AddWithValue("@MechanicId", mechanicId);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            
            var res=await cmd.ExecuteScalarAsync();
            
            return Convert.ToInt32(res);
        }
    }
}