using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.Enemy;

namespace Code.Utilities
{
    public class AbstractFactory<T> where T : IFactoryData
    {
        private static Dictionary<string, Type> _types;
        private static bool                     _isInitialized = false;

        /// <summary>
        /// Gathers all assemblies of T type
        /// </summary>
        /// <exception cref="Exception"></exception>
        private static void Initialize()
        {
            if(_isInitialized) return;
            
            var types = Assembly.GetAssembly(typeof(T)).GetTypes()
                                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(T)));

            _types = new Dictionary<string, Type>();

            foreach (var type in types)
            {
                var temp = Activator.CreateInstance(type) as IFactoryData;
                if (temp == null) throw new Exception();
                _types.Add(temp.Name, type);
            }

            _isInitialized = true;
        }
        
        /// <summary>
        /// Returns instance of given type
        /// </summary>
        /// <param name="aiTypeName">String representing AI which function should return</param>
        /// <returns></returns>
        /// <exception cref="IncorrectAIException">thrown when AI dont exists</exception>
        public static T Get(string typeName)
        {
            Initialize();
            if (_types.ContainsKey(typeName))
            {
                Type type = _types[typeName];
                var  instance   = (T)Activator.CreateInstance(type);
                return instance;
            }
            else
            {
                throw new IncorrectFactoryTypeException();
            }
        }
        
        /// <summary>
        /// Error thrown if factory don't have type which we want
        /// </summary>
        internal class IncorrectFactoryTypeException : CodeExecption
        {
            public IncorrectFactoryTypeException() : base(10001)
            {
            }
        }
    }
}