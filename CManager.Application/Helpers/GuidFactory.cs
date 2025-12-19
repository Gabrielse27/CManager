using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;



    namespace CManager.Application.Helpers;
   
        public static class GuidFactory
        {
              public static Guid Create()
              {
                 return Guid.NewGuid();
              }


        }
    
   

