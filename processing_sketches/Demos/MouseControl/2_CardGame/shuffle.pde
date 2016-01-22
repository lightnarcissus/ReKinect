
// fill an int array with a non-repeating random list (shuffle)
int[] shuffle(int num)
{
  int[] shuf = new int[num]; // this is the array with the shuffle list
  int[] used = new int[num]; // this is whether a number has already been used

  // none of the numbers should be used yet
  for(int i = 0;i<num;i++)
  {
    used[i]=0;
  }
  
  for(int i = 0;i<num;i++)
  {
    int keeppicking = 1;
    int pick = 0;
    while(keeppicking==1)
    {
      keeppicking=0;
      pick = int(random(0, num)); // pick a number
      if(used[pick]==1) keeppicking=1; // see if it's been used already... if yes, keep looping
    }
    
    shuf[i] = pick; // assign our random number to the shuffle list
    used[pick] = 1; // set that number to 'used' so it won't get picked again
    println(pick+ ""+ ".jpg");
  }
  
  return shuf; // toss back the array with the shuffle
  
}
