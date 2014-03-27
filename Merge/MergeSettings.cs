using System;
using System.Configuration;

namespace Merge
{
    public class MergeSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("0")]
        public int HighScore
        {
            get
            {
                return (int)this["HighScore"];
            }
            set
            {
                this["HighScore"] = (int)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("4")]
        public int GridWidth
        {
            get
            {
                return (int)this["GridWidth"];
            }
            set
            {
                this["GridWidth"] = (int)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("80")]
        public int TileWidth
        {
            get
            {
                return (int)this["TileWidth"];
            }
            set
            {
                this["TileWidth"] = (int)value;
            }
        }
    }
}
