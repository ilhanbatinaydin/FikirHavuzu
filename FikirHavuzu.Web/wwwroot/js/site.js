function clearFilterForm(element) {
    const form = element.closest('form');
    if (!form) return;

    form.reset();
    form.querySelectorAll('input').forEach(input => input.value = '');
    form.querySelectorAll('select').forEach(select => select.selectedIndex = 0);
}