using System;
using System.Threading;

// Define the delegate for the event
public delegate void AlarmEventHandler(object sender, EventArgs e);

// Publisher class
public class AlarmClock
{
    // Declare the event
    public event AlarmEventHandler raiseAlarm;
    
    private TimeSpan targetTime;
    
    public void SetAlarm(TimeSpan time)
    {
        targetTime = time;
    }
    
    public void StartMonitoring()
    {
        Console.WriteLine("Monitoring started...");
        
        while (true)
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            
            // Check if current time matches target time
            if (currentTime.Hours == targetTime.Hours && 
                currentTime.Minutes == targetTime.Minutes && 
                currentTime.Seconds == targetTime.Seconds)
            {
                // Raise the event
                OnRaiseAlarm();
                break;
            }
            
            // Wait for 1 second before checking again
            Thread.Sleep(1000);
        }
    }
    
    protected virtual void OnRaiseAlarm()
    {
        // Make a temporary copy of the event to avoid race conditions
        AlarmEventHandler handler = raiseAlarm;
        
        // Check if there are any subscribers
        if (handler != null)
        {
            // Raise the event
            handler(this, EventArgs.Empty);
        }
    }
}

// Subscriber class
public class Program
{
    // Event handler method
    public static void Ring_alarm(object sender, EventArgs e)
    {
        Console.WriteLine("ALARM! ALARM! ALARM! Time's up!");
    }
    
    public static void Main(string[] args)
    {
        // Create publisher instance
        AlarmClock alarmClock = new AlarmClock();
        
        // Subscribe to the event
        alarmClock.raiseAlarm += Ring_alarm;
        
        Console.WriteLine("Enter alarm time in HH:MM:SS format:");
        string input = Console.ReadLine();
        
        try
        {
            TimeSpan alarmTime = TimeSpan.Parse(input);
            alarmClock.SetAlarm(alarmTime);
            alarmClock.StartMonitoring();
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid time format. Please use HH:MM:SS");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
