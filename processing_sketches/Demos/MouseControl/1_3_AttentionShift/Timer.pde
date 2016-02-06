class Timer
{
  long startTime ; 
  long timeSoFar ; 
 
  boolean running ;
  int x, y ;
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
  }

  void continueRunning()
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