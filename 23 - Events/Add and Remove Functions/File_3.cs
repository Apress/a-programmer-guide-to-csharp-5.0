// 23 - Events\Add and Remove Functions
// copyright 2000 Eric Gunnerson
using System;
using System.Windows.Forms;
class KeyEventArgs: EventArgs
{
    Keys    keyData;
    
    KeyEventArgs(Keys keyData)
    {
        this.keyData = keyData;
    }
    
    public Keys KeyData
    {
        get
        {
            return(keyData);
        }
    }
    
    // other functions here
}