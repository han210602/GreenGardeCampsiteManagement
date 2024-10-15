<script>
    function switchTab(tabName) {
        // Ẩn tất cả các tab-content
        var tabContents = document.querySelectorAll('.tab-content');
    tabContents.forEach(function (content) {
        content.classList.remove('active');
        });

    // Ẩn tất cả các tab
    var tabs = document.querySelectorAll('.tab');
    tabs.forEach(function (tab) {
        tab.classList.remove('active');
        });

    // Hiện tab được chọn
    var activeTabContent = document.getElementById(tabName);
    if (activeTabContent) {
        activeTabContent.classList.add('active');
        }

        // Đánh dấu tab đang hoạt động
        var activeTab = Array.from(tabs).find(tab => tab.innerText === tabName);
    if (activeTab) {
        activeTab.classList.add('active');
        }
    }
</script>
