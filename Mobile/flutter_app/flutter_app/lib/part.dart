class Part {
  final int id;
  final String description;
  final String pictureId;

  Part._({
    this.id,
    this.description,
    this.pictureId,
  });

  factory Part.fromJson(Map<String, dynamic> json) {
    return new Part._(
        id: json['id'],
        description: json['description'],
        pictureId: json['pictureId'],
    );
  }
}