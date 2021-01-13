class Product {
  final int id;
  final String name;
  final String description;
  final String category;
  final String pictureId;

  Product._({
    this.id,
    this.name,
    this.description,
    this.category,
    this.pictureId
  });

  factory Product.fromJson(Map<String, dynamic> json) {
    return new Product._(
        id: json['id'],
        name: json['name'],
        description: json['description'],
        category: json['category'],
        pictureId: json['pictureId']
    );
  }
}