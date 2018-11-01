using P4Papi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4Papi.Repository
{
    public class DeathLineRepository : IDisposable
    {
        private PfourPEntities _ctx;

        public DeathLineRepository()
        {
            _ctx = new PfourPEntities();
        }

        public List<DeathLineListModel> GetDeathLineList(int numberOfRow)
        {
            var output = _ctx.DeathLines.OrderByDescending(ym => ym.YM).ToList();
            if (numberOfRow > 0)
                output = output.Take(numberOfRow).ToList();

            List<DeathLineListModel> deathLineList = new List<DeathLineListModel>();
            foreach (var deathLine in output)
            {
                string name = string.Empty;
                if (deathLine.ModifyUserId.HasValue)
                {
                    UserModel user = Repository.RepositoryFactory.UserRepository.GetUserData(deathLine.ModifyUserId.Value);
                    if (user != null)
                    {
                        name = user.Name;
                    }
                }
                DeathLineListModel deathLines = new DeathLineListModel(deathLine, name);
                deathLineList.Add(deathLines);
            }

            return deathLineList;
        }
        public DeathLine GetDeathLineByKeyValues(DateTime ym)
        {
            var output = _ctx.DeathLines.Where(dl => dl.YM == ym).ToList();
            if (output.Count > 0)
                return output[0];
            else
                return null;
        }
        public bool InsertDeathLine( DateTime ym, DateTime deathLine, int modifyUserId, out string error)
        {
            try
            {
                error = string.Empty;
                DeathLine newDeathLine = new DeathLine();
                newDeathLine.YM = ym;
                newDeathLine.DeathLineDate = deathLine;
                newDeathLine.ModifyUserId = modifyUserId;
                newDeathLine.ModifyDate = DateTime.Now;

                _ctx.DeathLines.Add(newDeathLine);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool UpdateDeathLine(DateTime ym, DateTime deathLine, int modifyUserId, out string error)
        {
            try
            {
                error = string.Empty;
                DeathLine updateDeathLine = GetDeathLineByKeyValues(ym);
                if (updateDeathLine != null)
                {
                    updateDeathLine.DeathLineDate = deathLine;
                    updateDeathLine.ModifyUserId = modifyUserId;
                    updateDeathLine.ModifyDate = DateTime.Now;
                    _ctx.SaveChanges();
                    return true;
                }
                else
                {
                    error = "No DeathLine data in database";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool DeleteDeathLine(DateTime ym, out string error)
        {
            try
            {
                error = string.Empty;
                DeathLine removeDeathLine = GetDeathLineByKeyValues(ym);
                _ctx.DeathLines.Remove(removeDeathLine);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool IsNotOverDeathLine(DateTime ym, DateTime currentDate)
        {
            bool result = false;
            var output = _ctx.DeathLines.Where(death => death.YM == ym && death.DeathLineDate >= currentDate);
            if (output.Count() > 0)
            {
                result = true;
            }
            return result;
        }

        public bool HasDeathLine(DateTime ym)
        {
            bool result = false;
            var output = _ctx.DeathLines.Where(death => death.YM == ym);
            if (output.Count() > 0)
            {
                result = true;
            }
            return result;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}