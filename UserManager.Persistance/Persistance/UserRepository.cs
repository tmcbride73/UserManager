using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserManager.Domain;

namespace UserManager.Infrastructure.Persistance
{
    public class UserRepository
    {
        private string DataFilePath { get; set; } = "wwwroot/data/MOCK_DATA.json";

        private async Task<IEnumerable<User>> GetLocalData()
        {
            var jsonData = await File.ReadAllLinesAsync(DataFilePath);
            var jsonDataString = string.Join(' ', jsonData);
            var userData = JsonConvert.DeserializeObject<IEnumerable<User>>(jsonDataString);
            return userData;
        }

        public async Task<User> Get(UserFilter filter)
        {
            var userData = await GetLocalData();

            var user = userData.Where(x =>
            x.SeqNum == filter.SeqNum
            || x.UserName == filter.UserName
            || x.FirstName == filter.FirstName
            || x.LastName == filter.LastName
            || x.Activity == filter.Activity ).FirstOrDefault();

            return user;
        }

        public async Task<IEnumerable<User>> GetAll(UserFilter filter)
        {
            var userData = await GetLocalData();

            //var users = userData.Where(x =>
            //x.UserName == filter.UserName
            //|| x.FirstName == filter.FirstName
            //|| x.LastName == filter.LastName
            //|| x.Activity == filter.Activity);

            return userData;
        }

        public async Task Insert(User entity)
        {
            long? nextSeqNum = 0;
            var userData = (await GetLocalData()).ToList();

            foreach (var user in userData)
            {
                if (user.UserName == entity.UserName)
                {
                    throw new Exception("User Name already exists.");
                }

                if (user.SeqNum >= nextSeqNum)
                {
                    nextSeqNum = user.SeqNum + 1;
                }
            }

            entity.SeqNum = nextSeqNum;

            userData.Add(entity);
            var userJson = JsonConvert.SerializeObject(userData);
            await File.WriteAllTextAsync(DataFilePath, userJson);
        }

        public async Task Update(User entity)
        {
            var userData = await GetLocalData();

            foreach (var user in userData)
            {
                if (user.SeqNum == entity.SeqNum)
                {
                    user.UserName = entity.UserName;
                    user.FirstName = entity.FirstName;
                    user.LastName = entity.LastName;
                    user.Activity = entity.Activity;
                }
            }

            var userJson = JsonConvert.SerializeObject(userData);
            await File.WriteAllTextAsync(DataFilePath, userJson);
        }
    }
}
