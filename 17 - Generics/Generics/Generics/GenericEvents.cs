#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Generics
{
 //generic delegate that takes sender and event args type paramters
 public delegate void StandaloneGenericDelegate<S, A>(S sender, A args);


 public class TimedEventArgs: EventArgs
 {
  //constructor
  public TimedEventArgs(DateTime time) { _timeOfEvent = time; }

  //member variable
  DateTime _timeOfEvent;

  //property
  public DateTime TimeOfEvent { get{ return _timeOfEvent; }}

 }

 public class TimedEventRaiser
 {
  public event StandaloneGenericDelegate<TimedEventRaiser, TimedEventArgs> TimedEvent;

  public void Raiser()
  {
   if (TimedEvent != null)
   {
    TimedEvent(this, new TimedEventArgs(DateTime.Now));
   }
  }
 }
}
