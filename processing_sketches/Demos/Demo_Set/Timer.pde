class Timer
{
  long startTime ; // time in msecs that timer started
  long timeSoFar ; // use to hold total time of run so far, useful in
  // conjunction with pause and continueRunning
  boolean running ;
  int x, y ; // location of timer output
  PFont font;

  Timer(int inX, int inY)
  {
    x = inX ;
    y = inY ;
    running = false ;
    timeSoFar = 0 ;
  }

  int currentTime()
  {
    if ( running )
      return ( (int) ( (millis() - startTime) / 1000.0) ) ;
    else
      return ( (int) (timeSoFar / 1000.0) ) ;
  }

  void start()
  {
    running = true ;
    startTime = millis() ;
    font = createFont("Helvetica-Bold", 80);
  }

  void restart()
    // reset the timer to zero and restart, identical to start
  {
    start() ;
  }

  void pause()
  {
    if (running)
    {
      timeSoFar = millis() - startTime ;
      running = false ;
    }
    // else do nothing, pause already called
  }

  void continueRunning()
    // called after stop to restart the timer running
    // no effect if already running
  {
    if (!running)
    {
      startTime = millis() - timeSoFar ;
      running = true ;
    }
  }

  void DisplayTime()
  {  
    int theTime ;
    String output = "";

    theTime = currentTime() ;
    output = output + theTime ;

    //  println("output = " + output) ;
    fill(255) ;
    textSize(120);
    textAlign(CENTER);
    textFont(font) ;
    if (theTime<10) {
      text("00:0"+output, x, y) ;
    } else {
      text("00:"+output, x, y) ;
    }
  }
}