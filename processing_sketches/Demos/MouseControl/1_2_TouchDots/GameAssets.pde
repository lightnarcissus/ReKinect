void toggleDone() {
}

void drawMenu() {

  textAlign(LEFT);
  textSize(60);
  fill(255);
  text("SCORE: " + score, 50, 120+15);

}


void reward_DrawingGame2() {
  if (score==3) {
    fill(255);
    textAlign(CENTER);
    textSize(200);
    text("+3!", width/2, height/2);
  }
  if (score==6) {
    fill(255);
    textAlign(CENTER);
    textSize(200);
    text("+6!", width/2, height/2);
  }
}