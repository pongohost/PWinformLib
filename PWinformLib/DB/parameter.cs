using System;

namespace PWinformLib
{
    public static class parameter
    {
        static String _server;
        static String _db;
        static String _usern;
        static String _pass;
        static String _port;

        public static string Server
        {
            get
            {
                return _server;
            }

            set
            {
                _server = value;
            }
        }

        public static string Db
        {
            get
            {
                return _db;
            }

            set
            {
                _db = value;
            }
        }

        public static string Usern
        {
            get
            {
                return _usern;
            }

            set
            {
                _usern = value;
            }
        }

        public static string Pass
        {
            get
            {
                return _pass;
            }

            set
            {
                _pass = value;
            }
        }

        public static string Port
        {
            get
            {
                return _port;
            }

            set
            {
                _port = value;
            }
        }
    }
}
