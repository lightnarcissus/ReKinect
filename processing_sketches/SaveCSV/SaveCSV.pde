Table table;
TableRow[] newRows;

void setup() {

  table = new Table();
  
  table.addColumn("Id");
  table.addColumn("User");
  table.addColumn("Problem Side");
  table.addColumn("Game");
  table.addColumn("Score");
  
  //TableRow newRow = table.addRow();
  
  ////newRow.setInt("User", table.getRowCount() - 1);
  //newRow.setString("User", "Caitlin");
  //newRow.setString("Problem Side", "Left");
  //newRow.setString("Game", "Drawing Challange");
  //newRow.setString("Score", "135");
  
   newRows = new TableRow[3];
  
   for (int i = 0; i < newRows.length; i++) {
     
    newRows[i] = table.addRow();
     
    newRows[i].setInt("Id", table.getRowCount());
    newRows[i].setString("User", "Claire");
    newRows[i].setString("Problem Side", "Left");
    newRows[i].setString("Game", "Drawing Challange");
    newRows[i].setInt("Score", int(random(0,300)));
   }
   
  saveTable(table, "data/new_4.csv");
}