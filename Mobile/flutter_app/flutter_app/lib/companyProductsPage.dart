import 'package:flutter/material.dart';
import 'productPage.dart';
import 'product.dart';
import 'main.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/scheduler.dart';
import 'dart:convert';

class CompanyProductsPage extends StatefulWidget {
  final int companyId;
  final String companyName;
  CompanyProductsPage(this.companyId, this.companyName) : super();
  @override
  _CompanyProductsPageState createState() => _CompanyProductsPageState(companyId, companyName);
}

class _CompanyProductsPageState extends State<CompanyProductsPage> {
  int companyId;
  String companyName;

  _CompanyProductsPageState(this.companyId, this.companyName);

  List<Product> list = List();
  final GlobalKey<RefreshIndicatorState> _refreshIndicatorKey =
  new GlobalKey<RefreshIndicatorState>();

  Future _fetchData() async {
    final response = await http.get(
        serverPath + "api/products?companyId=" + companyId.toString());
    if (response.statusCode == 200) {
      setState(() {
        list = (json.decode(response.body) as List)
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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Products of ' + companyName),
      ),
      drawer: DrawerMain(selected: "companies"),
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
                    FlatButton(
                      onPressed: () {
                        Navigator.pop(context);
                        Navigator.push(
                          context,
                          MaterialPageRoute(builder: (context) => ProductPage(list[index].id)),
                        );
                      },
                      child: Text(
                        list[index].name,
                        style: TextStyle(
                            fontSize: 14.0, fontWeight: FontWeight.bold),
                      ),
                    ),
                    Text(
                      list[index].category,
                      style: TextStyle(
                          fontSize: 11.0, fontWeight: FontWeight.normal),
                    ),
                    Text(
                      list[index].description,
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
    );
  }
}