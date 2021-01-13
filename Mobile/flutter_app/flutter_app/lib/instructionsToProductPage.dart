import 'package:flutter/material.dart';
import 'instruction.dart';
import 'main.dart';
import 'instructionPage.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/scheduler.dart';
import 'dart:convert';

class InstructionsToProductPage extends StatefulWidget {
  final int productId;
  final String productName;
  InstructionsToProductPage(this.productId, this.productName):super();
  @override
  _InstructionsToProductPageState createState() => _InstructionsToProductPageState(productId,productName);
}

class _InstructionsToProductPageState extends State<InstructionsToProductPage> {
  TextEditingController editingController = TextEditingController();
  int productId;
  String productName;
  _InstructionsToProductPageState(this.productId,this.productName);
  List<Instruction> list = List();
  List<Instruction> items = List();

  final GlobalKey<RefreshIndicatorState> _refreshIndicatorKey =
  new GlobalKey<RefreshIndicatorState>();

  Future _fetchData() async {
    final response = await http.get(
        serverPath + "api/instructions?productId=" + productId.toString());
    if (response.statusCode == 200) {
      setState(() {
        list = (json.decode(response.body) as List)
            .map((data) => new Instruction.fromJson(data))
            .toList();
        items = (json.decode(response.body) as List)
            .map((data) => new Instruction.fromJson(data))
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
    List<Instruction> dummySearchList = List<Instruction>();
    dummySearchList.addAll(list);
    if(query.isNotEmpty) {
      List<Instruction> dummyListData = List<Instruction>();
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
        title: Text('Instructions for ' + productName),
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
                          /*Image.network(
                            serverPath + items[index].pictureId,
                            width: 600.0,
                            height: 240.0,
                          ),*/
                          FlatButton(
                            onPressed: () {
                              Navigator.pop(context);
                              Navigator.push(
                                context,
                                MaterialPageRoute(builder: (context) => InstructionPage(items[index].id)),
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