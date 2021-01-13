import 'package:flutter/material.dart';
import  'productPage.dart';
import 'product.dart';
import 'main.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/scheduler.dart';
import 'dart:convert';

class PartProductsPage extends StatefulWidget {
  final int partId;
  final String partName;
  PartProductsPage(this.partId,this.partName):super();
  @override
  _PartProductsPageState createState() => _PartProductsPageState(partId, partName);
}

class _PartProductsPageState extends State<PartProductsPage> {
  TextEditingController editingController = TextEditingController();
  int partId;
  String partName;
  _PartProductsPageState(this.partId, this.partName);
  List<Product> list = List();
  List<Product> items = List();
  final GlobalKey<RefreshIndicatorState> _refreshIndicatorKey =
  new GlobalKey<RefreshIndicatorState>();

  Future _fetchData() async {
    final response = await http.get(
        serverPath + "api/partproduct?partId=" + partId.toString());
    if (response.statusCode == 200) {
      setState(() {
        list = (json.decode(response.body) as List)
            .map((data) => new Product.fromJson(data))
            .toList();
        items = (json.decode(response.body) as List)
            .map((data) => new Product.fromJson(data))
            .toList();
      });
    } else {
      throw Exception('Failed to load products');
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
    List<Product> dummySearchList = List<Product>();
    dummySearchList.addAll(list);
    if(query.isNotEmpty) {
      List<Product> dummyListData = List<Product>();
      dummySearchList.forEach((item) {
        if(item.name.contains(query)) {
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
        title: Text('Products with ' + partName),
      ),
      drawer: DrawerMain(selected: "products"),
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
                                MaterialPageRoute(builder: (context) => ProductPage(list[index].id)),
                              );
                            },
                            child: Text(
                              items[index].name,
                              style: TextStyle(
                                  fontSize: 14.0, fontWeight: FontWeight.bold),
                            ),
                          ),
                          Text(
                            items[index].category,
                            style: TextStyle(
                                fontSize: 11.0, fontWeight: FontWeight.normal),
                          ),
                          Text(
                            items[index].description,
                            style: TextStyle(
                                fontSize: 11.0, fontWeight: FontWeight.normal),
                          ),
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