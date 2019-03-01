
using Newtonsoft.Json;

namespace NannyTracker
{
    public class UserVm : ViewModelBase
    {
        public UserVm() {
            string strJson = "{'UserId':1,'ProfileImage':'abdelali.jpg','FirstName':'zekiri','LastName':'abdelali','EmailAddress':'zekiriabd@gmail.com'}";
            User user = JsonConvert.DeserializeObject<User>(strJson);
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.ProfileImage = user.ProfileImage;
        }

        public int Id { get; set; }

        public string _firstName { get; set; }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string _lastName { get; set; }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string _profileImage { get; set; }
        public string ProfileImage
        {
            get { return _profileImage; }
            set
            {
                _profileImage = value;
                OnPropertyChanged("ProfileImage");
            }
        }

        public string UserName { get { return this.LastName + " " + this.FirstName; } }
        
    }

}
