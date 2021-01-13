import 'package:flutter/material.dart';
import 'productPartsPage.dart';
import 'instructionsToProductPage.dart';
import 'product.dart';
import 'main.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/scheduler.dart';
import 'dart:convert';

class ProductPage extends StatefulWidget {
  final int productId;
  ProductPage(this.productId) : super();
  @override
  _ProductPageState createState() => _ProductPageState(productId);
}

class _ProductPageState extends State<ProductPage> {
  int productId;
  _ProductPageState(this.productId);
  List<Product> list = List();
  final GlobalKey<RefreshIndicatorState> _refreshIndicatorKey =
  new GlobalKey<RefreshIndicatorState>();

  Future _fetchData() async {
    final response = await http.get(
        serverPath + "api/products/" + productId.toString());
    if (response.statusCode == 200) {
      setState(() {
        if(list.length >= 1){
          list.removeLast();
        }
        list.add(Product.fromJson(json.decode(response.body)));
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
        title: Text('Product'),
      ),
      drawer: DrawerMain(selected: "products"),
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
                      list[index].name,
                      style: TextStyle(
                          fontSize: 14.0, fontWeight: FontWeight.bold),
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
                          MaterialPageRoute(builder: (context) => InstructionsToProductPage(list[index].id, list[index].name)),//передаввай два параметра название набора и его айди для получения листов инструкций
                        );
                      },
                      child: Text(
                        'Instruction',
                        style: TextStyle(
                            fontSize: 14.0, fontWeight: FontWeight.bold),
                      ),
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
                          MaterialPageRoute(builder: (context) => ProductPartsPage(list[index].id, list[index].name)),//передаввай два параметра название набора и его айди для получения деталей
                        );
                      },
                      child: Text(
                        'Parts',
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