using System;
using System.Runtime.InteropServices;
using System.Threading;
using X11;

namespace dwmBard.Servers
{
    // That is supposed to be a listener for common key like media keys or brightness adjustments.
    // It doesn't work now.
    // TODO: Make this work not too important tbh. (and i don't know X) so whatever.
    
    public class XEventListener
    {
        private Thread worker;
        private bool running = false;
        
        private IntPtr display;
        private Window root;

        public XEventListener()
        {
            worker = new Thread(start);
        }

        public void run()
        {
            worker.Start();
        }
        
        private void start()
        {
            if (!running)
            {
                running = true;
                
                if (!setupXConnection())
                    return;
                    
                processEvents();
            }

        }

        private bool setupXConnection()
        {
            display = Xlib.XOpenDisplay(null);

            if (display == IntPtr.Zero)
            {
                Console.WriteLine("Can't connect to X display.");
                running = false;
                return false;
            }

            root = Xlib.XDefaultRootWindow(display);

            Xlib.XSelectInput(display, root, 
                EventMask.KeyPressMask | EventMask.KeyReleaseMask);

            Xlib.XSync(display, false);
            
            return true;
        }

        private void processEvents()
        {
            IntPtr ev = Marshal.AllocHGlobal(24 * sizeof(long));
            Window currentWindow = Window.None;
            RevertFocus x=0;
            Xlib.XGetInputFocus(display, ref currentWindow, ref x);
            
            while (running)
            {
                Xlib.XNextEvent(display, ev);

                var xevent = Marshal.PtrToStructure<X11.XAnyEvent>(ev);

                Console.WriteLine("event!");
                switch (xevent.type)
                {
                    case (int) Event.ButtonPress:
                    {
                        var keyPressedEvent = Marshal.PtrToStructure<X11.XKeyEvent>(ev);
                        Console.WriteLine(keyPressedEvent);
                    }break;
                }
                
            }
        }
    }
}