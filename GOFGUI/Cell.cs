using System;
using System.ComponentModel;

namespace GOFGUI
{
   /*
    * Represents an individual cell in the GOL grid. Simply contains a 
    * boolean determining if the cell is alive or dead.
    * 
    * In a high-level sense, this is the Observable object, as the UI changes
    * depending on the status of the 'IsAlive' public boolean.
    */
   public class CellOfLife : INotifyPropertyChanged
   {

      //Private internal 'alive' boolean for cell.
      private bool _isAlive = false;


      /* Public boolean used to determine if the cell is alive or dead. 
       * 
       * Contains a PropertyChanged object to automatically inform the UI when the boolean
       * changes, so that the grid can be updated as required.
       */
      public bool IsAlive
      {
         get 
         { 
            //Return the private internal 'alive' boolean.
            return _isAlive; 
         }
                  
         set
         {
            //Set the internal 'alive' boolean.
            _isAlive = value;

            //Inform the UI of the changes to the boolean (i.e. Inform the Observer of a change in state)
            if (PropertyChanged != null)
            {
               PropertyChanged(this, new PropertyChangedEventArgs("IsAlive"));
            }
         }
      }

      #region INotifyPropertyChanged Members

      public event PropertyChangedEventHandler PropertyChanged;

      #endregion
   }
}
