using Dataaccess.Models;
using Repo.UOW.Unitofworkservices;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ViewModel
{
    public class Userviewmodel
    {
        private readonly Unitofwork _IUW;
        User Usr;
        public int ID { get; set; }
        public string VUsername { get; set; }
        public Userviewmodel()
        {
            _IUW = new Unitofwork();
            Usr = new User();
        }
        public List<User> LoadUsers()
        {
            var Datalist = _IUW.Users.GetAll();
            return Datalist.ToList();
        }
        public bool AddEditUsers(User _Usr)
        {
            try
            {
                Usr = _Usr;
                _IUW.Users.Add(_Usr);
                _IUW.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteUsers(int id)
        {
            try
            {
                _IUW.Users.Delbyid(id);
                _IUW.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        IEnumerable<User> Users;
        public List<User> GetUsers(string Username)
        {
            var Datalist = _IUW.Users.GetAll();
            if (!string.IsNullOrEmpty(Username))
            {
                var searchResults = Datalist.Where(p => p.Username.Contains(Username));
                Users = searchResults.ToList();
            }
            return Users.ToList();
        }
        public List<User> LoadUsercombo()
        {
            var usr = _IUW.Users.GetAll();
            return usr.ToList();
        }
        public bool GetUserlog(string Username, string Password)
        {
            var Flag = false;
            var Datalist = _IUW.Users.GetAll();
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                var searchResults = Datalist.Select(p => p.Username == Username &&
                                                        p.Password == Password).FirstOrDefault();
                if (searchResults)
                {
                    var usr = Datalist.Where(p => p.Username == Username).FirstOrDefault();
                    if (usr != null) {
                        ID = usr.Id;
                        VUsername = usr.Username;
                    }
                    Flag = true;
                }
                else
                {
                    Flag = false;
                }
            }
            return Flag;
        }
    }
}
