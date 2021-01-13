class PartDetails {
  final int id;
  final String description;
  final String pictureId;
  final String color;
  final int height;
  final int length;
  final String material;
  final int weight;
  final int width;

  PartDetails._({
    this.id,
    this.description,
    this.pictureId,
    this.color,
    this.height,
    this.length,
    this.material,
    this.weight,
    this.width,
  });

  factory PartDetails.fromJson(Map<String, dynamic> json) {
    return new PartDetails._(
      id: json['id'],
      description: json['description'],
      pictureId: json['pictureId'],
      color: json['color'],
      height: json['height'],
      length: json['length'],
      material: json['material'],
      weight: json['weight'],
      width: json['width'],
    );
  }
}