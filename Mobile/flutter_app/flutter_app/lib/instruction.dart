class Instruction {
  final int id;
  final String description;
  final int productId;

  Instruction._({
    this.id,
    this.description,
    this.productId,
  });

  factory Instruction.fromJson(Map<String, dynamic> json) {
    return new Instruction._(
      id: json['id'],
      description: json['description'],
      productId: json['productId'],
    );
  }
}