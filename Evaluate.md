Advantages  
- Project ngắn gọn, dễ đọc. Các hàm được đóng gói đúng tiêu chuẩn của một project OOP


Disadvantages 
- Tính đóng gói khiến cho việc kiểm soát dữ liệu khá khó khăn khi các class cần giao tiếp nhiều với nhau. 
- Class Item và Cell, và cả SpriteRenderer biểu thị cho ô rơi là 2 class tách biệt khiến việc Debug khó. 
Có thể cân nhắc việc sử dụng ma trận 2x2 với các item là số để dễ quản lý và truy cập nhanh chóng 
- Code hiện tại đang xử lý hàng ngang trước, sau đó nếu không clear được thì mới xử lý hàng dọc
nên sẽ dẫn đến việc thiếu logic, ví dụ khi ăn được hình chữ L hoặc dấu +