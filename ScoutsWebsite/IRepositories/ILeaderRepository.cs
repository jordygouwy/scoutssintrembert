using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutsWebsite.Models;

namespace ScoutsWebsite.IRepositories
{
    public interface ILeaderRepository
    {
        List<LeaderDetailItem> GetLeaders();
        void AddOrUpdateLeader(LeaderDetailItem model);
        LeaderDetailItem GetLeader(Guid id);
        void DeleteLeader(Guid id);
        byte[] GetLeaderPhoto(Guid id);
    }
}