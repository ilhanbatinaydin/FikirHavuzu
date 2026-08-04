$(document).ready(function () {

    $('.permission-checkbox').on('change', function () {
        let $this = $(this);
        let isChecked = $this.is(':checked');
        let currentId = parseInt($this.val());

        let dependencies = $this.data('dependencies');

        if (isChecked) {
            if (dependencies && dependencies.length > 0) {
                dependencies.forEach(function (requiredId) {
                    let $parentCheckbox = $('#perm_' + requiredId);

                    if (!$parentCheckbox.is(':checked')) {
                        $parentCheckbox.prop('checked', true);

                        $parentCheckbox.closest('.custom-checkbox-wrapper').fadeTo(100, 0.3).fadeTo(500, 1.0);

                        $parentCheckbox.trigger('change');
                    }
                });
            }
        }
        else {

            $('.permission-checkbox').each(function () {
                let $otherCheckbox = $(this);

                if ($otherCheckbox.val() != currentId && $otherCheckbox.is(':checked')) {

                    let otherDependencies = $otherCheckbox.data('dependencies');

                    if (otherDependencies && otherDependencies.includes(currentId)) {
                        $otherCheckbox.prop('checked', false);

                        $otherCheckbox.closest('.custom-checkbox-wrapper').fadeTo(100, 0.3).fadeTo(500, 1.0);

                        $otherCheckbox.trigger('change');
                    }
                }
            });
        }
    });

});