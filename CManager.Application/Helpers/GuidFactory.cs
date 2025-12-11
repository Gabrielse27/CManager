using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;



    namespace CManager.Application.Helpers;
   
        public class GuidFactory
        {
              public Guid CreateGuid()
             {
                 return Guid.NewGuid();
              }


        }
    
   

