using System;

namespace ApplicationContext
{
    /// <summary>
    /// SpringContext it's a easy-simple-helper class that implement a singleton,
    /// and help to get the objects for the application.
    /// </summary>
    public class SpringContext
    {
        /// <summary>
        /// Object that gonna content the context of the app
        /// </summary>
        private Spring.Context.IApplicationContext _SpringContext = null;

        /// <summary>
        /// Needed recursive declaration to implement a singleton
        /// </summary>
        private static SpringContext _SPContext;

        private SpringContext()
        {
            try
            {
                this._SpringContext = Spring.Context.Support.ContextRegistry.GetContext();
            }
            catch (Exception e)
            {
                throw new Exception("Can't get the context of the Application " + e.Message);
            }
        }

        /// <summary>
        /// Provide a Unique instance of SpringContext
        /// </summary>
        public static SpringContext Instance
        {
            get
            {

                if (_SPContext == null)
                {
                    _SPContext = new SpringContext();
                }

                return _SPContext;

            }
        }

        /// <summary>
        /// Return a instance of object in the context by a given name
        /// </summary>
        /// <param name="objectName">Name of the solicited object</param>
        /// <returns>A instance of the object, in other case an exception</returns>
        public object GetObject(string objectName)
        {
            return this._SpringContext.GetObject(objectName);
        }

    }
}