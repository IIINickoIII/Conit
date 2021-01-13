import 'package:flutter/material.dart';
import 'part.dart';
import 'main.dart';
import 'partPage.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/scheduler.dart';
import 'dart:convert';

class PartsPage extends StatefulWidget {
  @override
  _PartsPageState createState() => _PartsPageState();
}

class _PartsPageState extends State<PartsPage> {
  TextEditingController editingController = TextEditingController();

  List<Part> list = List();
  List<Part> items = List();

  final GlobalKey<RefreshIndicatorState> _refreshIndicatorKey =
  new GlobalKey<RefreshIndicatorState>();

  Future _fetchData() async {
    final response = await http.get(
        serverPath + "api/parts");
    if (response.statusCode == 200) {
      setState(() {
        list = (json.decode(response.body) as List)
            .map((data) => new Part.fromJson(data))
            .toList();
        items = (json.decode(response.body) as List)
            .map((data) => new Part.fromJson(data))
            .toList();
      });
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

  void filterSearchResults(String query) {
    List<Part> dummySearchList = List<Part>();
    dummySearchList.addAll(list);
    if(query.isNotEmpty) {
      List<Part> dummyListData = List<Part>();
      dummySearchList.forEach((item) {
        if(item.description.contains(query)) {
          dummyListData.add(item);
        }
      });
      setState(() {
        items.clear();
        items.addAll(dummyListData);
      });
      return;
    } else {
      setState(() {
        items.clear();
        items.addAll(list);
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Parts'),
      ),
      drawer: DrawerMain(selected: "parts"),
      body: RefreshIndicator(
        key: _refreshIndicatorKey,
        onRefresh: _fetchData,
        child: Column(
          children: <Widget>[
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: TextField(
                onChanged: (value) {
                    filterSearchResults(value);
                },
                controller: editingController,
                decoration: InputDecoration(
                    labelText: "Search",
                    hintText: "Search",
                    prefixIcon: Icon(Icons.search),
                    border: OutlineInputBorder(
                        borderRadius: BorderRadius.all(Radius.circular(25.0)))),
              ),
            ),
            Expanded(
              child: ListView.builder(
                shrinkWrap: true,
                itemCount: items.length,
                itemBuilder: (BuildContext context, int index) {
                  return Card(
                    child: Container(
                      padding: EdgeInsets.all(16.0),
                      child: Column(
                        children: <Widget>[
                          Image.network(
                            serverPath + items[index].pictureId,
                            width: 600.0,
                            height: 240.0,
                          ),
                          FlatButton(
                            onPressed: () {
                              Navigator.pop(context);
                              Navigator.push(
                                context,
                                MaterialPageRoute(builder: (context) => PartPage(items[index].id)),
                              );
                            },
                            child: Text(
                              items[index].description,
                              textAlign: TextAlign.center,
                              style: TextStyle(
                                  fontSize: 14.0, fontWeight: FontWeight.bold),
                            ),
                          )
                        ],
                      ),
                    ),
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}