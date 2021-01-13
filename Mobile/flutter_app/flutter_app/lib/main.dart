import 'package:flutter/material.dart';
import 'productsPage.dart';
import 'companiesPage.dart';
import 'partsPage.dart';
import 'instructionsPage.dart';


String serverPath = 'https://4d57b959.ngrok.io/';
//String serverPath = 'https://localhost:44397/';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'CONIT',
      theme: ThemeData(
        // ...
        primarySwatch: Colors.blue,
      ),
      home: MyHomePage(),
    );
  }
}

class MyHomePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('About'),
      ),
      drawer: DrawerMain(selected: "about"),
      body: Center(
        child: Text(
          //'Welcome to CONIT app!',
          serverPath,
        ),
      ),
    );
  }
}

class DrawerMain extends StatefulWidget {
  DrawerMain({Key key, this.selected}) : super(key: key);

  final String selected;

  @override
  DrawerMainState createState() {
    return DrawerMainState();
  }
}

class DrawerMainState extends State<DrawerMain> {
  @override
  Widget build(BuildContext context) {
    return Drawer(
        child: ListView(padding: EdgeInsets.zero, children: <Widget>[
          DrawerHeader(
            child: Text(
              'CONIT',
              style: TextStyle(
                color: Colors.white,
                fontSize: 32.0,
              ),
            ),
            decoration: BoxDecoration(
              color: Colors.blue,
            ),
          ),
          ListTile(
            selected: widget.selected == 'about',
            leading: Icon(Icons.info),
            title: Text('About'),
            onTap: () {
              Navigator.pop(context);
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => MyHomePage()),
              );
            },
          ),
          ListTile(
            selected: widget.selected == 'companies',
            leading: Icon(Icons.list),
            title: Text('Companies'),
            onTap: () {
              Navigator.pop(context);
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => CompaniesPage()),
              );
            },
          ),
          ListTile(
            selected: widget.selected == 'products',
            leading: Icon(Icons.list),
            title: Text('Products'),
            onTap: () {
              Navigator.pop(context);
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => ProductsPage()),
              );
            },
          ),
          ListTile(
            selected: widget.selected == 'parts',
            leading: Icon(Icons.list),
            title: Text('Parts'),
            onTap: () {
              Navigator.pop(context);
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => PartsPage()),
              );
            },
          ),
          ListTile(
            selected: widget.selected == 'instructions',
            leading: Icon(Icons.list),
            title: Text('Instructions'),
            onTap: () {
              Navigator.pop(context);
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => InstructionsPage()),
              );
            },
          ),
        ]));
  }
}




