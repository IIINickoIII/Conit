import 'package:flutter/material.dart';
import 'partProductsPage.dart';
import 'partDetails.dart';
import 'main.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/scheduler.dart';
import 'dart:convert';

class PartPage extends StatefulWidget {
  final int id;
  PartPage(this.id) : super();
  @override
  _PartPageState createState() => _PartPageState(id);
}

class _PartPageState extends State<PartPage> {
  int id;
  _PartPageState(int partId){
    this.id = partId;
  }
  List<PartDetails> list = List();
  final GlobalKey<RefreshIndicatorState> _refreshIndicatorKey =
  new GlobalKey<RefreshIndicatorState>();

  Future _fetchData() async {
    final response = await http.get(
        serverPath + "api/parts/" + id.toString());
    if (response.statusCode == 200) {
      setState(() {
        if(list.length >= 1){
          list.removeLast();
        }
        list.add(PartDetails.fromJson(json.decode(response.body)));
      });
      //return Part.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to load parts');
    }
  }

  @override
  void initState() {
    super.initState();
    SchedulerBinding.instance.addPostFrameCallback((_) {
      _refreshIndicatorKey.currentState?.show();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Part'),
      ),
      drawer: DrawerMain(selected: "part"),
      body: RefreshIndicator(
        key: _refreshIndicatorKey,
        onRefresh: _fetchData,
        child: ListView.builder(
          itemCount: list.length,
          itemBuilder: (BuildContext context, int index) {
            return Card(
              child: Container(
                padding: EdgeInsets.all(16.0),
                child: Column(
                  children: <Widget>[
                    Image.network(
                      serverPath + list[index].pictureId,
                      width: 600.0,
                      height: 240.0,
                    ),
                    Text(
                      list[index].description,
                      textAlign: TextAlign.center,
                      style: TextStyle(
                          fontSize: 14.0, fontWeight: FontWeight.bold),
                    ),
                    Text(
                      'Material: ' + list[index].material,
                      textAlign: TextAlign.center,
                      style: TextStyle(
                          fontSize: 11.0, fontWeight: FontWeight.normal),
                    ),
                    Text(
                      'Color: ' + list[index].color,
                      textAlign: TextAlign.center,
                      style: TextStyle(
                          fontSize: 11.0, fontWeight: FontWeight.normal),
                    ),
                    Text(
                      'Length: ' + list[index].length.toString() + ' mm',
                      textAlign: TextAlign.center,
                      style: TextStyle(
                          fontSize: 11.0, fontWeight: FontWeight.normal),
                    ),
                    Text(
                      'Width: ' + list[index].width.toString() + ' mm',
                      textAlign: TextAlign.center,
                      style: TextStyle(
                          fontSize: 11.0, fontWeight: FontWeight.normal),
                    ),
                    Text(
                      'Height: ' + list[index].height.toString() + ' mm',
                      textAlign: TextAlign.center,
                      style: TextStyle(
                          fontSize: 11.0, fontWeight: FontWeight.normal),
                    ),
                    Text(
                      'Weight: ' + list[index].weight.toString() + ' gm',
                      textAlign: TextAlign.center,
                      style: TextStyle(
                          fontSize: 11.0, fontWeight: FontWeight.normal),
                    ),
                    FlatButton(
                      color: Colors.blue,
                      textColor: Colors.white,
                      disabledColor: Colors.grey,
                      disabledTextColor: Colors.black,
                      padding: EdgeInsets.all(8.0),
                      splashColor: Colors.blueAccent,
                      onPressed: () {
                        Navigator.pop(context);
                        Navigator.push(
                          context,
                          MaterialPageRoute(builder: (context) => PartProductsPage(list[index].id, list[index].description)),//передаввай два параметра название детали и айди для отображения наборов с этой деталью
                        );
                      },
                      child: Text(
                        'Products with this part',
                        style: TextStyle(
                            fontSize: 14.0, fontWeight: FontWeight.bold),
                      ),
                    ),
                  ],
                ),
              ),
            );
          },
        ),
      ),
    );
  }
}