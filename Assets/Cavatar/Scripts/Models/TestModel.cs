using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


namespace Cavater.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class Ranking
    {
        [DataMember(Name = "max_score")]
        public long MaxScore;
        [DataMember(Name = "min_score")]
        public long MinScore;
    }

    /// <summary>
    /// Userをキーにして、ユーザーのランキングデータをただ入れるだけ
    /// </summary>
    public class UserScoreRawData
    {
        [DataMember(Name = "id")]
        public string UserID;

        [DataMember(Name = "username")]
        public string Username;

        [DataMember(Name = "score")]
        public long Score;
    }
}
