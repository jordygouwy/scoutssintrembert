using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutsWebsite.IRepositories;
using ScoutsWebsite.Models;
using System.Drawing;
using System.IO;
using System.Data.Linq;
using ScoutsWebsite.Properties;
using System.Drawing.Imaging;

namespace ScoutsWebsite.Repositories
{
    public class LeaderRepository : ILeaderRepository
    {
        private ScoutsDataDataContext _db;

        public LeaderRepository(ScoutsDataDataContext db)
        {
            this._db = db;
        }

        public List<Models.LeaderDetailItem> GetLeaders()
        {
            var leaders = (from x in _db.Leaders
                           orderby x.LeaderTakType ascending, x.LeaderTakLeader descending
                           select new LeaderDetailItem()
                           {
                               LeaderEmail = x.LeaderEmail,
                               LeaderFirstName = x.LeaderFirstName,
                               LeaderID = x.LeaderID,
                               LeaderLastName = x.LeaderLastName,
                               LeaderPhone = x.LeaderPhone,
                               Type = x.LeaderTakType,
                               LeaderTakLeader = ((x.LeaderTakLeader ?? 0) == 1)
                           }).ToList();

            return leaders;
        }


        public void AddOrUpdateLeader(LeaderDetailItem model)
        {

            var leader = (from x in _db.Leaders where x.LeaderID == model.LeaderID select x).FirstOrDefault();
            if (leader == null)
            {
                leader = new Leader();
                leader.LeaderID = Guid.NewGuid();
                _db.Leaders.InsertOnSubmit(leader);
            }

            if (model.File != null && model.File.ContentLength > 0)
            {
                Bitmap bitmap = new Bitmap(model.File.InputStream);
                Image image = bitmap.GetThumbnailImage(100, 100, null, new IntPtr());
                string tempfile = Path.GetTempFileName();
                image.Save(tempfile);
                using (FileStream stream = File.OpenRead(tempfile))
                {
                    byte[] buff = new byte[stream.Length];
                    stream.Read(buff, 0, (int)stream.Length);
                    leader.LeaderPhoto = new Binary(buff);
                }
            }

            leader.LeaderEmail = model.LeaderEmail ?? "";
            leader.LeaderFirstName = model.LeaderFirstName ?? "";
            leader.LeaderLastName = model.LeaderLastName ?? "";
            leader.LeaderPhone = model.LeaderPhone ?? "";
            leader.LeaderTakLeader = model.LeaderTakLeader ? (short)1 : (short)0;
            leader.LeaderTakType = model.Type;
            _db.SubmitChanges();
        }


        public LeaderDetailItem GetLeader(Guid id)
        {
            var leader = (from x in _db.Leaders
                          where x.LeaderID == id
                          select new LeaderDetailItem()
                          {
                              LeaderEmail = x.LeaderEmail,
                              LeaderFirstName = x.LeaderFirstName,
                              LeaderID = x.LeaderID,
                              LeaderLastName = x.LeaderLastName,
                              LeaderPhone = x.LeaderPhone,
                              Type = x.LeaderTakType,
                              LeaderTakLeader = ((x.LeaderTakLeader ?? 0) == 1)
                          }).FirstOrDefault();
            return leader;
        }


        public void DeleteLeader(Guid id)
        {
            var leader = (from x in _db.Leaders
                          where x.LeaderID == id
                          select x).FirstOrDefault();
            if (leader != null)
            {
                _db.Leaders.DeleteOnSubmit(leader);
                _db.SubmitChanges();
            }
        }


        public byte[] GetLeaderPhoto(Guid id)
        {
            try
            {
                var leader = (from l in _db.Leaders
                              where l.LeaderID == id
                              select new { l.LeaderPhoto }).FirstOrDefault();

                if (leader != null && leader.LeaderPhoto != null && leader.LeaderPhoto.Length > 0)
                {
                    return leader.LeaderPhoto.ToArray();
                }
                else
                {
                    MemoryStream ms = new MemoryStream();
                    Resources.unknown.Save(ms, ImageFormat.Png);
                    return ms.ToArray();


                    // return ms.ToArray();
                    //File.OpenRead(new Uri("~/Content/images/unknown.png", UriKind.RelativeOrAbsolute).ToString());
                }
            }
            catch (Exception ex)
            {
   
            }
            return null;
        }
    }
}