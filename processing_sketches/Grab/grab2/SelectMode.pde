void selectMode(){
  
  //rightHand
  //leftHand
  //both
}

void buttons(){
  fill(255,255,0);
  rect(75,75,50,50);
  rect(75+60,75,50,50);
  fill(0);
  textSize(45);
  text("L",75,90);
  text("R",75+60,90);
}

void mouseReleased(){
if(mouseX > 50 && mouseX< 100 && mouseY> 50 && mouseY< 100 ){ 
 fill(255,0,0);
 rect(75,75,50,50);
 
} 
else if(mouseX> 110 && mouseX< 160 && mouseY> 50 && mouseY< 100 ){ 
 fill(255,0,0);
 rect(75+60,75,50,50);
}
}