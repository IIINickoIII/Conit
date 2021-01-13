import 'package:flutter/material.dart';
import 'instructionPart.dart';
import 'partPage.dart';
import 'main.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/scheduler.dart';
import 'dart:convert';

class InstructionPage extends StatefulWidget {
  final int instructionId;
  InstructionPage(this.instructionId):super();
  @override
  _InstructionPageState createState() => _InstructionPageState(instructionId);
}

class _InstructionPageState extends State<InstructionPage> {
  TextEditingController editingController = TextEditingController();
  int instructionId;
  _InstructionPageState(this.instructionId);
  List<InstructionPart> list = List();
  List<InstructionPart> items = List();

  final GlobalKey<RefreshIndicatorState> _refreshIndicatorKey =
  new GlobalKey<RefreshIndicatorState>();

  Future _fetchData() async {
    final response = await http.get(
        serverPath + "api/instructionpages?instructionId=" + instructionId.toString());
    if (response.statusCode == 200) {
      setState(() {
        list = (json.decode(response.body) as List)
            .map((data) => new InstructionPart.fromJson(data))
            .toList();
        items = (json.decode(response.body) as List)
            .map((data) => new InstructionPart.fromJson(data))
            .toList();
      });
    } else {
      throw Exception('Failed to load InstructionParts');
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
    List<InstructionPart> dummySearchList = List<InstructionPart>();
    dummySearchList.addAll(list);
    if(query.isNotEmpty) {
      List<InstructionPart> dummyListData = List<InstructionPart>();
      dummySearchList.forEach((item) {
        if(item.pageNumber.toString().contains(query)) {
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
        title: Text('Pages'),
      ),
      drawer: DrawerMain(selected: "instructions"),
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
                          Text(
                            'Page number ' + items[index].pageNumber.toString(),
                            textAlign: TextAlign.center,
                            style: TextStyle(
                                fontSize: 14.0, fontWeight: FontWeight.bold),
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
                                MaterialPageRoute(builder: (context) => PartPage(items[index].partId)),
                              );
                            },
                            child: Text(
                              'Part description',
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